using System;
using System.Collections.Generic;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.DataAccess.InMemory
{
    public class CacheRepository : ICacheRepository
    {
        private static readonly Lazy<CacheRepository> _instance = new Lazy<CacheRepository>(() => new CacheRepository());

        public static CacheRepository Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

        private CacheRepository() { }

        public object this[string key]
        {
            get
            {
                object result;
                _storage.TryGetValue(key, out result);
                return result;
            }
            set
            {
                if (value == null)
                    _storage.Remove(key);
                else
                    _storage[key] = value;
            }
        }
    }
}
