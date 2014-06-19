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
        private static List<Type> _sourceTypes;
        private readonly List<ISource> _sources = new List<ISource>();

        public CoreFacade(Settings settings)
        {
            LoadSourceTypes();
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
            return provider.GetWeather(dateRange, location);
        }

        public IEnumerable<ISource> GetSources()
        {
            return _sources;
        }

        private static void LoadSourceTypes()
        {
            var iSource = typeof(ISource);
            _sourceTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(iSource.IsAssignableFrom)
                .Where(t => !t.IsInterface)
                .ToList();
        }
    }
}
