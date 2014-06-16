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
            var res = f.GetWeather(new DateRange { From = new DateTime(2008, 04, 12, 12, 53, 0), To = new DateTime(2008, 04, 12, 12, 53, 0) }, new Location { Latitude = 50f, Longitude = 50f });
            Assert.IsNotEmpty(res);
        }
    }
}
