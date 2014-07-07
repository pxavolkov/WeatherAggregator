using System;
using System.Collections.Generic;
using System.Linq;
using WeatherAggregator.Core.Aspects;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;
using WeatherAggregator.Core.Logic;

namespace WeatherAggregator.Core
{
    public class CoreFacade
    {
        private readonly List<ISource> _sources;
        private readonly Func<IFeedbackRepository> _feedbackRepositoryFactory;

        public CoreFacade(Settings settings)
        {
            _sources = settings.Sources;
            MethodCacheAttribute.CacheRepositoryFactory = settings.CacheRepositoryFactory;
            _feedbackRepositoryFactory = settings.FeedbackRepositoryFactory;
        }

        public List<Weather> GetWeather(List<Guid> sources, DateRange dateRange, Location location)
        {
            var provider = new WeatherProvider(_sources.Where(s => sources.Contains(s.Id)).ToList());
            return provider.GetWeather(dateRange, location);
        }

        public IEnumerable<ISource> GetSources()
        {
            return _sources;
        }

        public void AddFeedback(Feedback feedback)
        {
            var provider = new FeedbackProvider(_feedbackRepositoryFactory());
            provider.Add(feedback);
        }
    }
}
