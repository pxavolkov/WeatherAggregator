using System.Collections.Generic;
using System.Linq;
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
            return _sources.SelectMany(s => s.GetWeather(dateRange, location)).GroupBy(r => r.Date).Select(g => new Weather
            {
                Temperature = (int) g.Average(r => r.Temperature),
                Cloudness = (int) g.Average(r => r.Cloudness),
                Precipitation = (Precipitation) (int) g.Average(r => (int) r.Precipitation)
            }).ToList();
        }
    }
}
