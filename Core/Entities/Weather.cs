using System;

namespace WeatherAggregator.Core.Entities
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public int Cloudness { get; set; }
        public Precipitation Precipitation { get; set; }
    }
}
