using System;
using NUnit.Framework;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Sources.OpenWeatherMap;

namespace Tests
{
    [TestFixture]
    public class OpenWeatherMapSourceTest
    {
        [Test]
        public void TheTest()
        {
            var source = new OpenWeatherMapSource();
            var weather = source.GetWeather(
                new DateRange { From = DateTime.Now, To = DateTime.Now.AddDays(1) },
                new Location { Latitude = 44.5833, Longitude = 33.5167 }
            );

            Assert.AreEqual(1, weather.Count);
        }
    }
}
