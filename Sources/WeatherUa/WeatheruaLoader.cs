using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Sources;

namespace WeatherAggregator.Sources.WeatherUa
{
    public static class WeatheruaLoader
    {

        public static List<WeatheruaCity> GetCities()
        {
            List<WeatheruaCity> cities = null;
            if (File.Exists(ConfigurationManager.AppSettings["WeatherUaCitiesPath"]))
            {
                using (var reader = new StreamReader(ConfigurationManager.AppSettings["WeatherUaCitiesPath"]))
                {
                    cities = JsonConvert.DeserializeObject<List<WeatheruaCity>>(reader.ReadToEnd());
                }
            }
            if (cities == null)
            {
                cities = LoadCities();
                SaveCities(cities);
            }

            return cities;
        }

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

        private static void SaveCities(List<WeatheruaCity> weatheruaCities)
        {
            var json = JsonConvert.SerializeObject(weatheruaCities);

            using (var writer = new StreamWriter(ConfigurationManager.AppSettings["WeatherUaCitiesPath"]))
            {
                writer.Write(json);
            }
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
