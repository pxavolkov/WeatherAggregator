using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace Sources
{
    public class WeatheruaSource: ISource
    {
        public void Dispose()
        {
        }

        public Guid Id { get { return new Guid("F1465F7E-DFAC-4D77-A717-0613B05A5281"); } }
        public string Name { get { return "Weather.ua source"; } }
        public string Snippet { get { return "<a href='weather.ua>Weather.ua</a>"; } }
        public List<Weather> GetWeather(DateRange dateRange, Location location)
        {
            throw new NotImplementedException();
        }

        public static List<WeatheruaCity> GetCitiesIDs()
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
            var result = cities.Select(x => new WeatheruaCity { ID = x.Attribute("id").Value, 
                Name = x.Descendants().First(n => n.Name.LocalName == "name").Value,
                NameEn = x.Descendants().First(n => n.Name.LocalName == "name_en").Value,
                Region = x.Descendants().First(n => n.Name.LocalName == "region").Value,
                Country = x.Descendants().First(n => n.Name.LocalName == "country").Value
            }).ToList();

            return result;
        }
    }
}
