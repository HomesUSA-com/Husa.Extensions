namespace Husa.Extensions.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;

    public class InMemoryCache : ICache
    {
        private const string DefaultCachePrefix = "HusaCache";

        public void Clear()
        {
            foreach (var cachedElement in MemoryCache.Default)
            {
                MemoryCache.Default.Remove(cachedElement.Key);
            }
        }

        public object Get(string key, bool withPrefix = false)
        {
            return MemoryCache.Default[GetPrefixedKey(key, withPrefix)];
        }

        public IDictionary<string, object> GetValues(IEnumerable<string> keys)
        {
            return MemoryCache.Default.GetValues(keys.Select(GetPrefixedKey));
        }

        public void Insert(string key, object value, int seconds, bool withPrefix = false)
        {
            this.Insert(key, value, TimeSpan.FromSeconds(seconds), withPrefix);
        }

        public void Insert(string key, object value, TimeSpan expiration, bool withPrefix = false)
        {
            var prefixedKey = GetPrefixedKey(key, withPrefix);

            MemoryCache.Default.Set(prefixedKey, value, new CacheItemPolicy
            {
                Priority = CacheItemPriority.Default,
                AbsoluteExpiration = DateTimeOffset.UtcNow.Add(expiration),
            });
        }

        public void Remove(string key, bool withPrefix = false)
        {
            if (!this.Contains(key, withPrefix))
            {
                return;
            }

            var prefixedKey = GetPrefixedKey(key, withPrefix);
            MemoryCache.Default.Remove(prefixedKey);
        }

        public bool Contains(string key, bool withPrefix = false)
        {
            return MemoryCache.Default.Contains(GetPrefixedKey(key, withPrefix));
        }

        private static string GetPrefixedKey(string key) => GetPrefixedKey(key, false);

        private static string GetPrefixedKey(string key, bool withPrefix) => withPrefix ? key : $"{DefaultCachePrefix}_{key}";
    }
}
