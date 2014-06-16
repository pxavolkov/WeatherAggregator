using System;
using NUnit.Framework;
using Sources;
using WeatherAggregator.Core.Entities;

namespace Tests
{
    [TestFixture]
    public class ForecastioTests
    {
        [Test]
        public void GetWeatherTest()
        {
            var f = new ForecastioSource();
            var res = f.GetWeather(new DateRange { From = DateTime.Now, To = DateTime.Now.AddDays(1) }, new Location { Latitude = 37.8267f, Longitude = -122.423f });
            Assert.IsNotEmpty(res);
        }
    }
}
