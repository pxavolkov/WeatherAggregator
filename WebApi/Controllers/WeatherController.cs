using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using WeatherAggregator.Core;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.WebApi.Models;

namespace WeatherAggregator.WebApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly CoreFacade _facade = CreateCoreFacade();

        [HttpGet]
        public List<SourceModel> Sources()
        {
            return _facade.GetSources().Select(SourceModel.Map).ToList();
        }

        [HttpPost]
        public List<Weather> GetWeather(WeatherRequest request)
        {
            var result = _facade.GetWeather(request.Sources, request.DateRange, request.Location);
            return result;
        }

        private static CoreFacade CreateCoreFacade()
        {
            var settings = new Settings
            {
                CacheRepositoryFactory = () => DataAccess.InMemory.CacheRepository.Instance,
                WeatherCacheTimeoutSeconds = int.Parse(ConfigurationManager.AppSettings["WeatherCacheTimeoutSeconds"])
            };
            return new CoreFacade(settings);
        }
    }
}