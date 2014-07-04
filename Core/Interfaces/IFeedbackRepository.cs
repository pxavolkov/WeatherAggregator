using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.Core.Interfaces
{
    public interface IFeedbackRepository
    {
        void Add(Feedback feedback);
    }
}
