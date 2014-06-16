using System;
using System.Collections.Generic;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.WebApi.Models
{
    public class WeatherRequest
    {
        public List<Guid> Sources { get; set; }
        public DateRange DateRange { get; set; }
        public Location Location { get; set; }
    }
}