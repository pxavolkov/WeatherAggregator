using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sources;
using WeatherAggregator.Sources.Aspects;

namespace WeatherAggregator.Sources.WeatherUa
{
    public static class WeatheruaLoader
    {
        [CityCache]
        public static List<WeatheruaCity> LoadCities()
        {
            List<WeatheruaCity> weatheruaCities = new List<WeatheruaCity>();

            var xml = XDocument.Load("http://xml.weather.co.ua/1.2/country/");
            var countries = xml.Descendants().Where(x => x.Name.LocalName == "country" && x.Attribute("id") != null).Select(x => x.Attribute("id").Value).ToList();
            foreach (var country in countries)
            {
                xml = XDocument.Load("http://xml.weather.co.ua/1.2/city/?country=" + country);
                var cities = xml.Descendants().Where(x => x.Name.LocalName == "city" && x.Attribute("id") != null).ToList();
                weatheruaCities.AddRange(Convert(cities));
            }

            return weatheruaCities;
        }

        private static IEnumerable<WeatheruaCity> Convert(IEnumerable<XElement> cities)
        {
            var result = cities.Select(x => new WeatheruaCity
            {
                ID = x.Attribute("id").Value,
                Name = x.Descendants().First(n => n.Name.LocalName == "name").Value,
                NameEn = x.Descendants().First(n => n.Name.LocalName == "name_en").Value,
                Region = x.Descendants().First(n => n.Name.LocalName == "region").Value,
                Country = x.Descendants().First(n => n.Name.LocalName == "country").Value
            }).ToList();

            return result;
        }
    }
}
