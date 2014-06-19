using System;

namespace WeatherAggregator.Core.Entities
{
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public bool Contains(DateTime date)
        {
            return date >= From && date <= To;
        }

        public DateRange Normalize()
        {
            return new DateRange { From = From.Date, To = To.Date };
        }
    }
}
