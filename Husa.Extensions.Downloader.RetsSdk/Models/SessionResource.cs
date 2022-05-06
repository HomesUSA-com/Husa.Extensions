namespace Husa.Extensions.Downloader.RetsSdk.Models
{
    using Husa.Extensions.Downloader.RetsSdk.Exceptions;
    using Husa.Extensions.Downloader.RetsSdk.Models.Enums;
    using System;
    using System.Collections.Generic;

    public class SessionResource
    {
        public string SessionId { get; set; }
        public string Cookie { get; set; }

        public Dictionary<Capability, Uri> Capabilities { get; set; }

        public SessionResource()
        {
            Capabilities = new Dictionary<Capability, Uri>();
        }

        public void AddCapability(Capability name, string url, UriKind uriType)
        {
            var uri = new Uri(url);

            if (Capabilities.ContainsKey(name) || !uri.IsWellFormedOriginalString())
            {
                return;
            }
            
            Capabilities.TryAdd(name, uri);
        }

        public Uri GetCapability(Capability name)
        {
            if(!Capabilities.ContainsKey(name))
            {
                throw new MissingCapabilityException();
            }

            return Capabilities[name];
        }

    }
}
