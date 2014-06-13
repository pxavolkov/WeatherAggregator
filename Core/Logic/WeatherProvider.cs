using System.Collections.Generic;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Logic
{
    public class WeatherProvider
    {
        private readonly List<ISource> _sources;

        public WeatherProvider(List<ISource> sources)
        {
            _sources = sources;
        }

        public List<Weather> GetWeather(DateRange dateRange, Location location)
        {
            return null;
        }
    }
}
