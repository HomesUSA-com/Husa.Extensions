namespace Husa.Extensions.Downloader.RetsSdk.Services
{
    using Microsoft.Extensions.Logging;
    using MimeKit;
    using MimeTypes.Core;
    using Husa.Extensions.Downloader.RetsSdk.Contracts;
    using Husa.Extensions.Downloader.RetsSdk.Exceptions;
    using Husa.Extensions.Downloader.RetsSdk.Helpers.Extensions;
    using Husa.Extensions.Downloader.RetsSdk.Models;
    using Husa.Extensions.Downloader.RetsSdk.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Xml.Linq;

    public class RetsClient : RetsResponseBase<RetsClient>, IRetsClient
    {
        private readonly IRetsRequester Requester;
        private readonly IRetsSession Session;

        protected Uri GetObjectUri => Session.Resource.GetCapability(Capability.GetObject);
        protected Uri SearchUri => Session.Resource.GetCapability(Capability.Search);
        protected Uri GetMetadataUri => Session.Resource.GetCapability(Capability.GetMetadata);
        protected int MarketLimit => Session.GetMarketLimit();


        public RetsClient(IRetsSession session, IRetsRequester requester, ILogger<RetsClient> logger)
            : base(logger)
        {
            Session = session ?? throw new ArgumentNullException($"{nameof(session)} cannot be null");
            Requester = requester ?? throw new ArgumentNullException($"{nameof(requester)} cannot be null");
        }

        public async Task Connect()
        {
            await Session.Start();
        }

        public async Task Disconnect()
        {
            await Session.End();
        }

        /// <summary>
        /// Searches using the criteria of the request parameter, performs a batch query search forcing a
        /// round trip and using MarketLimit as batch size to prevent having too many outstanding requests.
        /// </summary>
        /// <param name="request">Search criteria.</param>
        /// <returns> Returns a SearchResult containing the results of the search</returns>
        public async Task<SearchResult> Search(SearchRequest request)
        {
            var offset = 1;

            RetsResource resource = await GetResourceMetadata(request.SearchType);
            var result = await Search(request, offset);
            offset += MarketLimit;

            while (offset <= result.TotalCount)
            {
                SearchResult _results = await RoundTrip(async () =>
                {
                    return await Search(request, offset);
                });

                foreach (var row in _results.GetRows())
                {
                    result.AddRow(row);
                }
                offset += MarketLimit;
            }

            return result;
        }

        private async Task<SearchResult> Search(SearchRequest request, int offset)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{request} cannot be null");
            }

            RetsResource resource = await GetResourceMetadata(request.SearchType);

            if (resource == null)
            {
                string message = string.Format("The provided '{0}' is not valid. You can get a list of all valid value by calling '{1}' method on the Session object.", nameof(SearchRequest.SearchType), nameof(GetResourcesMetadata));

                throw new Exception(message);
            }

            var uriBuilder = new UriBuilder(SearchUri);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("SearchType", request.SearchType);
            query.Add("Class", request.Class);
            query.Add("QueryType", request.QueryType);
            query.Add("Count", request.Count.ToString());
            query.Add("Format", request.Format);
            query.Add("Limit", MarketLimit.ToString());
            query.Add("Offset", offset.ToString());
            query.Add("StandardNames", request.StandardNames.ToString());
            query.Add("RestrictedIndicator", request.RestrictedIndicator);
            query.Add("Query", request.ParameterGroup.ToString());

            if (request.HasColumns())
            {
                var columns = request.GetColumns().ToList();

                if (!request.HasColumn(resource.KeyField))
                {
                    columns.Add(resource.KeyField);
                }

                query.Add("Select", string.Join(",", columns));
            }

            uriBuilder.Query = query.ToString();

            return await Requester.Get(uriBuilder.Uri, async (response) =>
            {
                using (Stream stream = await GetStream(response))
                {
                    XDocument doc = XDocument.Load(stream);

                    int code = GetReplayCode(doc.Root);

                    AssertValidReplay(doc.Root, code);

                    var result = new SearchResult(resource, request.Class, request.RestrictedIndicator);

                    if (code == 0)
                    {
                        char delimiterValue = GetCompactDelimiter(doc);
                        result.SetTotalCount(GetCount(doc));

                        XNamespace ns = doc.Root.GetDefaultNamespace();
                        XElement columns = doc.Descendants(ns + "COLUMNS").FirstOrDefault();

                        IEnumerable<XElement> records = doc.Descendants(ns + "DATA");

                        string[] tableColumns = columns.Value.Split(delimiterValue);
                        result.SetColumns(tableColumns);
                        
                        if (resource.ResourceId == "Media")
                        {
                            resource.KeyField = "PHOTOKEY";
                        }

                        foreach (var record in records)
                        {
                            string[] fields = record.Value.Split(delimiterValue);

                            SearchResultRow row = new SearchResultRow(tableColumns, fields, resource.KeyField, request.RestrictedIndicator);

                            result.AddRow(row);
                        }
                    }

                    return result;
                }
            }, Session.Resource);
        }

        public async Task<RetsSystem> GetSystemMetadata()
        {
            var uriBuilder = new UriBuilder(GetMetadataUri);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("Type", "METADATA-SYSTEM");
            query.Add("ID", "*");
            query.Add("Format", "STANDARD-XML");

            uriBuilder.Query = query.ToString();

            return await Requester.Get(uriBuilder.Uri, async (response) =>
            {
                using (Stream stream = await GetStream(response))
                {
                    XDocument doc = XDocument.Load(stream);

                    AssertValidReplay(doc.Root);

                    XNamespace ns = doc.Root.GetDefaultNamespace();

                    XElement metaData = doc.Descendants(ns + "METADATA").FirstOrDefault();

                    XElement metadataSystem = metaData.Elements().FirstOrDefault();
                    XElement systemMeta = metadataSystem.Elements().FirstOrDefault();

                    XElement metaDataResource = metadataSystem.Descendants("METADATA-RESOURCE").FirstOrDefault();

                    RetsResourceCollection resources = new RetsResourceCollection();
                    resources.Load(metaDataResource);

                    var system = new RetsSystem()
                    {
                        SystemId = systemMeta.Attribute("SystemID")?.Value,
                        SystemDescription = systemMeta.Attribute("SystemDescription")?.Value,

                        Version = metadataSystem.Attribute("Version")?.Value,
                        Date = DateTime.Parse(metadataSystem.Attribute("Date")?.Value),
                        Resources = resources
                    };

                    return system;
                }
            }, Session.Resource);
        }

        public async Task<RetsResourceCollection> GetResourcesMetadata()
        {
            RetsResourceCollection capsule = await MakeMetadataRequest<RetsResourceCollection>("METADATA-RESOURCE", "0");

            return capsule;
        }

        public async Task<RetsResource> GetResourceMetadata(string resourceId)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException($"{resourceId} cannot be null.");
            }

            RetsResourceCollection capsule = await GetResourcesMetadata();

            var resource = capsule.Get().FirstOrDefault(x => x.ResourceId.Equals(resourceId, StringComparison.CurrentCultureIgnoreCase)) ?? throw new ResourceDoesNotExists();

            return resource;
        }

        public async Task<RetsClassCollection> GetClassesMetadata(string resourceId)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException($"{resourceId} cannot be null.");
            }

            return await MakeMetadataRequest<RetsClassCollection>("METADATA-CLASS", resourceId);
        }

        public async Task<RetsObjectCollection> GetObjectMetadata(string resourceId)
        {
            return await MakeMetadataRequest<RetsObjectCollection>("METADATA-OBJECT", resourceId);
        }


        public async Task<RetsLookupTypeCollection> GetLookupValues(string resourceId, string lookupName)
        {
            return await MakeMetadataRequest<RetsLookupTypeCollection>("METADATA-LOOKUP_TYPE", string.Format("{0}:{1}", resourceId, lookupName));
        }

        public async Task<IEnumerable<RetsLookupTypeCollection>> GetLookupValues(string resourceId)
        {
            return await MakeMetadataCollectionRequest<RetsLookupTypeCollection>("METADATA-LOOKUP_TYPE", resourceId);
        }

        public async Task<RetsFieldCollection> GetTableMetadata(string resourceId, string className)
        {
            return await MakeMetadataRequest<RetsFieldCollection>("METADATA-TABLE", string.Format("{0}:{1}", resourceId, className));
        }

        public async Task<IEnumerable<FileObject>> GetObject(string resource, string type, PhotoId id, bool useLocation = false)
        {
            return await GetObject(resource, type, new List<PhotoId> { id }, useLocation);
        }


        public async Task<IEnumerable<FileObject>> GetObject(string resource, string type, IEnumerable<PhotoId> ids, int batchSize, bool useLocation = false)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException($"{nameof(resource)} cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException($"{nameof(type)} cannot be null.");
            }

            if (ids == null)
            {
                throw new ArgumentNullException($"{nameof(ids)} cannot be null.");
            }

            List<FileObject> files = new List<FileObject>();

            IEnumerable<IEnumerable<PhotoId>> pages = ids.Partition(batchSize);

            foreach (var page in pages)
            {
                // To prevent having to many outstanding requests
                // we should connect, force round trip on every page

                IEnumerable<FileObject> _files = await RoundTrip(async () =>
                {
                    return await GetObject(resource, type, page, useLocation);
                });

                files.AddRange(_files);
            }

            return files;
        }

        public async Task RoundTrip(Func<Task> action)
        {
            try
            {
                if (!Session.IsStarted())
                {
                    await Connect();
                }

                action?.Invoke();

            }
            catch
            {
                throw;
            }
            finally
            {
                await Disconnect();
            }
        }


        public async Task<TResult> RoundTrip<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                if (!Session.IsStarted())
                {
                    await Connect();
                }

                TResult result = await action.Invoke();
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                await Disconnect();
            }
        }



        public async Task<IEnumerable<FileObject>> GetObject(string resource, string type, IEnumerable<PhotoId> ids, bool useLocation = false)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException($"{nameof(resource)} cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException($"{nameof(type)} cannot be null.");
            }

            if (ids == null)
            {
                throw new ArgumentNullException($"{nameof(ids)} cannot be null.");
            }

            var uriBuilder = new UriBuilder(GetObjectUri);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("Resource", resource);
            query.Add("Type", type);
            query.Add("ID", string.Join(',', ids.Select(x => x.ToString())));
            query.Add("Location", useLocation ? "1" : "0");
            query.Add("ObjectData", "*");

            uriBuilder.Query = query.ToString();

            return await Requester.Get(uriBuilder.Uri, async (response) =>
            {
                string responseContentType = response.Content.Headers.ContentType.ToString(); // GetValues("Content-Type").FirstOrDefault();

                var files = new List<FileObject>();

                if (!ContentType.TryParse(responseContentType, out ContentType documentContentType))
                {
                    return files;
                }

                using (Stream memoryStream = await GetStream(response))
                {
                    if (documentContentType.MediaSubtype.Equals("xml", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // At this point we know there is a problem because Mime response is expected not XML.
                        XDocument doc = XDocument.Load(memoryStream);

                        AssertValidReplay(doc.Root);

                        return files;
                    }

                    MimeEntity entity = MimeEntity.Load(documentContentType, memoryStream);

                    if (entity is Multipart multipart)
                    {
                        // At this point we know this is a multi-image response

                        foreach (MimePart part in multipart.OfType<MimePart>())
                        {
                            files.Add(ProcessMessage(part));
                        }

                        return files;
                    }

                    if (entity is MimePart message)
                    {
                        // At this point we know this is a single image response
                        files.Add(ProcessMessage(message));
                    }
                }

                return files;

            }, Session.Resource);
        }


        protected async Task<T> MakeMetadataRequest<T>(string type, string id, string format = "STANDARD-XML")
            where T : class, IRetsCollectionXElementLoader
        {
            var uriBuilder = new UriBuilder(GetMetadataUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("Type", type);
            query.Add("ID", id);
            query.Add("Format", format);

            uriBuilder.Query = query.ToString();

            return await Requester.Get(uriBuilder.Uri, async (response) => await ParseMetadata<T>(response), Session.Resource);
        }


        protected async Task<IEnumerable<T>> MakeMetadataCollectionRequest<T>(string type, string resourceId, string format = "STANDARD-XML")
            where T : class, IRetsCollectionXElementLoader
        {
            var uriBuilder = new UriBuilder(GetMetadataUri);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("Type", type);
            query.Add("ID", $"{resourceId}:*");
            query.Add("Format", format);

            uriBuilder.Query = query.ToString();

            return await Requester.Get(uriBuilder.Uri, async (response) => await ParseMetadataCollection<T>(response), Session.Resource);
        }

        protected async Task<T> ParseMetadata<T>(HttpResponseMessage response)
            where T : class, IRetsCollectionXElementLoader
        {
            using (Stream stream = await GetStream(response))
            {
                XDocument doc = XDocument.Load(stream);

                AssertValidReplay(doc.Root);

                XNamespace ns = doc.Root.GetDefaultNamespace();

                T collection = (T)Activator.CreateInstance(typeof(T));

                XElement metaData = doc.Descendants(ns + "METADATA").FirstOrDefault();

                if (metaData != null)
                {
                    // INSTEAD OF FirstOrDefault
                    // loop over all the elements
                    XElement metaDataNode = metaData.Elements().FirstOrDefault();
                    if (metaDataNode != null)
                    {
                        collection.Load(metaDataNode);
                    }
                }

                return collection;
            }
        }


        protected async Task<IEnumerable<T>> ParseMetadataCollection<T>(HttpResponseMessage response)
            where T : class, IRetsCollectionXElementLoader
        {
            using (Stream stream = await GetStream(response))
            {
                XDocument doc = XDocument.Load(stream);

                AssertValidReplay(doc.Root);

                XNamespace ns = doc.Root.GetDefaultNamespace();

                var list = new List<T>();

                XElement metaData = doc.Descendants(ns + "METADATA").FirstOrDefault();

                if (metaData != null)
                {
                    // INSTEAD OF FirstOrDefault
                    // loop over all the elements
                    foreach (XElement metaDataNode in metaData.Elements())
                    {
                        T collection = (T)Activator.CreateInstance(typeof(T));

                        collection.Load(metaDataNode);

                        list.Add(collection);
                    }
                }

                return list;
            }
        }


        protected char GetCompactDelimiter(XDocument doc)
        {
            XNamespace ns = doc.Root.GetDefaultNamespace();
            XElement delimiter = doc.Descendants(ns + "DELIMITER").FirstOrDefault();

            if (delimiter == null)
            {
                throw new RetsParsingException("Unable to find the delimiter! Only 'COMPACT' or 'COMPACT-DECODED' are supported when querying the data");
            }

            var delimiterAttribute = delimiter.Attribute("value");

            if (delimiterAttribute != null && int.TryParse(delimiterAttribute.Value, out int value))
            {
                return Convert.ToChar(value);
            }

            return Convert.ToChar(9);
        }

        protected int GetCount(XDocument doc)
        {
            XNamespace ns = doc.Root.GetDefaultNamespace();
            XElement count = doc.Descendants(ns + "COUNT").FirstOrDefault();
            if (count == null)
            {
                throw new RetsParsingException("Unable to find count!");
            }

            var countAttribute = count.Attribute("Records");
            if (countAttribute != null && int.TryParse(countAttribute.Value, out int value))
            {
                return value;
            }

            return 0;
        }

        protected FileObject ProcessMessage(MimePart message)
        {
            var file = new FileObject()
            {
                ContentId = message.ContentId,
                ContentType = new System.Net.Mime.ContentType(message.ContentType.MimeType),
                ContentDescription = message.Headers["Content-Description"],
                ContentSubDescription = message.Headers["Content-Sub-Description"],
                ContentLocation = message.ContentLocation,
                MemeVersion = message.Headers["MIME-Version"],
                Extension = MimeTypeMap.GetExtension(message.ContentType.MimeType)
            };

            if (int.TryParse(message.Headers["Object-ID"], out int objectId))
            {
                file.ObjectId = objectId;
            }
            else
            {
                // This should never happen
                throw new RetsParsingException("For some reason Object-ID does not exists in the response or it is not an integer value as expected");
            }

            if (bool.TryParse(message.Headers["Preferred"], out bool isPreferred))
            {
                file.IsPreferred = isPreferred;
            }

            if (message.ContentLocation == null)
            {
                file.Content = new MemoryStream();
                message.Content.DecodeTo(file.Content);
                file.Content.Position = 0; // This is important otherwise the next seek with start at the end
            }

            return file;
        }
    }
}
