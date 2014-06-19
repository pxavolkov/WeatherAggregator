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

        /// <summary>
        /// Max number of days forecast. 0 - unlimited.
        /// </summary>
        int ForecastMaxDays { get; }
        List<Weather> GetWeather(DateRange dateRange, Location location);
    }
}
