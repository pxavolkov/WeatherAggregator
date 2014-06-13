using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class WeatherController : ApiController
    {
        public List<Source> GetSources()
        {
            return new List<Source> { new Source { } };
        }

        public Weather GetWeather(List<int> sources, DateRange dateRange, Location location)
        {
            return new Weather();
        }
    }
}