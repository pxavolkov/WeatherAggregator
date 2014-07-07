using System;
using System.Collections.Generic;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Entities
{
    public class Settings
    {
        public Func<ICacheRepository> CacheRepositoryFactory;
        public Func<IFeedbackRepository> FeedbackRepositoryFactory;
        public List<ISource> Sources;
    }
}
