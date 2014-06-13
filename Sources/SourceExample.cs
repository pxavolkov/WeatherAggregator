using System;
using System.Collections.Generic;

using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace Sources
{
    public class SourceExample : ISource
    {
        #region Public Properties

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Snippet { get; private set; }

        #endregion

        #region Ctor

        public SourceExample()
        {
            Id = Guid.NewGuid();
            Name = "Sevastopol";
            Snippet = "<a href=\"www.google.com\">Source Example</a>";
        }

        #endregion


        #region Public Methods

        public List<Weather> GetWeather(DateRange dateRange, Location location)
        {
            List<Weather> result = new List<Weather>();


            result.Add(new Weather {Date = DateTime.Now, Temperature = 22, Cloudness = 50});

            return result;

        }

        #endregion
    }
}

