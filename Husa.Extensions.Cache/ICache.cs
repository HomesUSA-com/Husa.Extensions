namespace Husa.Extensions.Cache
{
    using System;
    using System.Collections.Generic;

    public interface ICache
    {
        object Get(string key, bool withPrefix = false);

        void Clear();

        void Insert(string key, object value, int seconds, bool withPrefix = false);

        void Insert(string key, object value, TimeSpan expiration, bool withPrefix = false);

        void Remove(string key, bool withPrefix = false);

        bool Contains(string key, bool withPrefix = false);

        IDictionary<string, object> GetValues(IEnumerable<string> keys);
    }
}
