using System;
using System.Collections.Generic;
using System.Linq;
using WeatherAggregator.Core.Aspects;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;
using WeatherAggregator.Core.Logic;

namespace WeatherAggregator.Core
{
    public class CoreFacade : IDisposable
    {
        private static readonly List<Type> _sourceTypes;
        private readonly List<ISource> _sources = new List<ISource>();

        static CoreFacade()
        {
            var iSource = typeof(ISource);
            _sourceTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(iSource.IsAssignableFrom)
                .Where(t => !t.IsInterface)
                .ToList();
        }

        public CoreFacade(Settings settings)
        {
            foreach (var sourceType in _sourceTypes)
            {
                var source = (ISource) Activator.CreateInstance(sourceType);
                _sources.Add(source);
            }

            MethodCacheAttribute.CacheRepositoryFactory = settings.CacheRepositoryFactory;
            MethodCacheAttribute.CacheTimeoutSeconds = settings.WeatherCacheTimeoutSeconds;
        }

        public List<Weather> GetWeather(List<Guid> sources, DateRange dateRange, Location location)
        {
            var provider = new WeatherProvider(_sources.Where(s => sources.Contains(s.Id)).ToList());
            return provider.GetWeather(dateRange.Normalize(), location);
        }

        public IEnumerable<ISource> GetSources()
        {
            return _sources;
        }

        public void Dispose()
        {
            foreach (var source in _sources)
            {
                source.Dispose();
            }
        }
    }
}
