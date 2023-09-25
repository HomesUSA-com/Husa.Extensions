namespace Husa.Extensions.Downloader.Trestle.Helpers
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Common;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;
    using Husa.Extensions.Downloader.Trestle.Models.TableEntities;

    public static class Utils
    {
        private const string PAGESIZE = "1000";
        private const string EXPANDPAGESIZE = "200";

        public static bool IsValidToken(TokenEntity tokenInfo)
        {
            return tokenInfo != null
                && !string.IsNullOrEmpty(tokenInfo.AccessToken)
                && tokenInfo.ExpireDate >= DateTimeOffset.Now;
        }

        public static string GetFilter<T>(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false)
        {
            if (modificationTimestamp == null && string.IsNullOrEmpty(filter))
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var filterUrl = $"?&$top={(expand ? EXPANDPAGESIZE : PAGESIZE)}";
            var modificationTime = modificationTimestamp.HasValue ? $"ModificationTimestamp ge {string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", modificationTimestamp)}" : null;
            if (typeof(Property) == typeof(T))
            {
                filterUrl = filterUrl.AddQueryStrings("filter", new List<string> { filter, modificationTime });
                filterUrl = filterUrl.AddQueryString("expand", "OpenHouse,Rooms", expand);
            }
            else if (typeof(Media) == typeof(T))
            {
                filterUrl = filterUrl.AddQueryString("filter", $"ResourceRecordKey in ({filter})", !string.IsNullOrEmpty(filter));
            }
            else if (typeof(PropertyRooms) == typeof(T) || typeof(OpenHouse) == typeof(T))
            {
                filterUrl = filterUrl.AddQueryString("filter", $"ListingKey in ({filter})", !string.IsNullOrEmpty(filter));
            }
            else
            {
                filterUrl = filterUrl.AddQueryStrings("filter", new List<string> { modificationTime, filter });
            }

            return filterUrl;
        }

        public static string AddSystemOriginFilter(string filter, SystemOrigin? systemOrigin)
        {
            return systemOrigin is null ? filter : filter + $" and OriginatingSystemName eq '{((SystemOrigin)systemOrigin).ToStringFromEnumMember()}'";
        }
    }
}
