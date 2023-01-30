namespace Husa.Extensions.Downloader.Trestle.Helpers
{
    using System;
    using Husa.Extensions.Downloader.Trestle.Models;
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


            string filterUrl;
            var top = $"?&$top={(expand ? EXPANDPAGESIZE : PAGESIZE)}";
            var timeFilter = modificationTimestamp != null
                ? $"&$filter=ModificationTimestamp ge {string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", modificationTimestamp)}"
                : string.Empty;
            if (typeof(Property) == typeof(T))
            {
                var expandFilter = expand ? "&$expand=OpenHouse,Rooms" : string.Empty;
                var localFilter = !string.IsNullOrEmpty(filter) ? timeFilter + $" and {filter}" : timeFilter;
                filterUrl = top + expandFilter + localFilter;
            }
            else if (typeof(Media) == typeof(T))
            {
                var localFilter = $"&$filter=ResourceRecordKey in ({filter})";
                filterUrl = top + localFilter;
            }
            else if (typeof(PropertyRooms) == typeof(T) || typeof(OpenHouse) == typeof(T))
            {
                var localFilter = $"&$filter=ListingKey in ({filter})";
                filterUrl = top + localFilter;
            }
            else
            {
                var localFilter = !string.IsNullOrEmpty(filter) ? timeFilter + $" and {filter}" : timeFilter;
                filterUrl = top + localFilter;
            }

            return filterUrl;
        }
    }
}
