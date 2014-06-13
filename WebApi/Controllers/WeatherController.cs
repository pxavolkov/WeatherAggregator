﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WeatherAggregator.Core;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.WebApi.Models;

namespace WeatherAggregator.WebApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly CoreFacade _facade = new CoreFacade();

        [HttpGet]
        public List<SourceModel> Sources()
        {
            return _facade.GetSources().Select(SourceModel.Map).ToList();
        }

        [HttpPost]
        public List<Weather> GetWeather(List<Guid> sources, DateRange dateRange, Location location)
        {
            return _facade.GetWeather(sources, dateRange, location);
        }

        ~WeatherController()
        {
            _facade.Dispose();
        }
    }
}