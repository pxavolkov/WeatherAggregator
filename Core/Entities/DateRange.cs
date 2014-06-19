using System;
using WeatherAggregator.Core.Aspects;

namespace WeatherAggregator.Core.Entities
{
    public class DateRange
    {
        [DateOnly]
        public DateTime From { get; set; }

        [DateOnly]
        public DateTime To { get; set; }

        public bool Contains(DateTime date)
        {
            return date >= From && date <= To;
        }
    }
}
