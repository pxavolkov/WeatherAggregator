using System;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Logic
{
    public class FeedbackProvider
    {
        private readonly IFeedbackRepository _feedback;

        public FeedbackProvider(IFeedbackRepository feedback)
        {
            _feedback = feedback;
        }

        public void Add(Feedback feedback)
        {
            feedback.DateCreated = DateTime.UtcNow;
            _feedback.Add(feedback);
        }
    }
}
