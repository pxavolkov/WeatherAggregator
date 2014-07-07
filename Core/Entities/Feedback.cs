using System;

namespace WeatherAggregator.Core.Entities
{
    public class Feedback
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}
