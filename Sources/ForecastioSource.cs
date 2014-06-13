using System;
using System.Collections.Generic;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace Sources
{
    public class ForecastioSource:ISource
    {
        public Guid Id { get { return new Guid("2D498C9F-8294-4A47-97E4-6CF1B1800F3E"); } }
        public string Name { get { return "Forecast.io source";  } }
        public string Snippet { get { return "<a href='http://forecast.io/'>Powered by Forecast</a>"; } }
        public List<Weather> GetWeather(DateRange dateRange, Location location)
        {
            var result = new List<Weather>();
            var day = dateRange.From;
            while (day < dateRange.To)
            {
                var w = new Weather{Date = day, Temperature = 15, Cloudness = 30};
                result.Add(w);
                day = day.AddDays(1);
            }

            return result;
        }
    }
}
