using System;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Entities
{
    public class Settings
    {
        public int WeatherCacheTimeoutSeconds;
        public Func<ICacheRepository> CacheRepositoryFactory;
    }
}
