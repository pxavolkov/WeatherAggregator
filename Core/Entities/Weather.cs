using System;

namespace WeatherAggregator.Core.Entities
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public int Cloudness { get; set; }
    }
}
