using System.Web.Http;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.WebApi.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public void Add(Feedback feedback)
        {
            Facade.AddFeedback(feedback);
        }
    }
}