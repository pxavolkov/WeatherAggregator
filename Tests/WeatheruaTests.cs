using System;
using NUnit.Framework;
using Sources;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Sources.WeatherUa;

namespace Tests
{
    [TestFixture]
    public class WeatheruaTests
    {
        [Test]
        public void LoadXml()
        {
            var r = WeatheruaLoader.LoadCities();
        }
        
        [Test]
        public void GetWeather()
        {
            WeatheruaSource w = new WeatheruaSource();
            var res = w.GetWeather(new DateRange { From = new DateTime(2014, 06, 18, 00, 53, 0), To = new DateTime(2014, 06, 20, 00, 53, 0) }, new Location { Latitude = 50f, Longitude = 50f, City = "Севастополь"});

        }
    }
}
