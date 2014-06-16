using System.Collections.Generic;

namespace WeatherAggregator.Sources.OpenWeatherMap.Api
{
    class WeatherItem
    {
        public long dt;
        public int clouds;
        public Temperature temp;
        public List<Summary> weather;
    }
}
