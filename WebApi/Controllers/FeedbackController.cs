using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.WebApi.Controllers
{
    public class FeedbackController : BaseController
    {
        public void Add(Feedback feedback)
        {
            Facade.AddFeedback(feedback);
        }
    }
}