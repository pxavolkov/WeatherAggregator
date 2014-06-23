using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using WeatherAggregator.Core;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;
using WeatherAggregator.Sources.Aspects;
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
                Sources = LoadSources()
            };

            return new CoreFacade(settings);
        }

        private static List<ISource> LoadSources()
        {
            var iSource = typeof(ISource);
            var sources = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .SelectMany(s => s.GetTypes())
                .Where(iSource.IsAssignableFrom)
                .Where(t => !t.IsInterface)
                .Select(t => (ISource) Activator.CreateInstance(t))
                .ToList();
            return sources;
        }
    }
}