using System;
using System.Collections.Generic;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.Core.Interfaces
{
    public interface ISource
    {
        Guid Id { get; }
        string Name { get; }
        string Snippet { get; }
        List<Weather> GetWeather(DateRange dateRange, Location location);
    }
}
