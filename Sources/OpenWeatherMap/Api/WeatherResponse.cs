using System.Collections.Generic;

namespace WeatherAggregator.Sources.OpenWeatherMap.Api
{
    class WeatherResponse
    {
        public string cod;
        public List<WeatherItem> list;
    }
}
