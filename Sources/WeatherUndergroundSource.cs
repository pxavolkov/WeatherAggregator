using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;
using WeatherAggregator.Sources.WeatherUnderground;

namespace WeatherAggregator.Sources
{
    public class WeatherUndergroundSource : ISource
    {
  
        #region Public Properties

        public Guid Id { get { return new Guid("b0fce04e-4cd6-4ba9-8e75-4e8f1a51c33e"); } }
        public string Name { get { return "Weather Undergroud API"; } }
        public string Snippet { get { return "<a href='http://http://www.wunderground.com/'>Powered by Weather Underground</a>"; } }

        #endregion


        #region Public Methods

        public List<Weather> GetWeather(DateRange dateRange, Location location)
        {
            // dateRange is not used because method returns forecast for today and three more days

            var locationUrl = GetLocationByLatLng(location);
            ThreeDaysForecastModel model = ThreeDaysForecast(locationUrl);
            List<Weather> result = ConvertForecastModel(model);

            return result;
        }

        public void Dispose()
        {

        }

        #endregion

        private string ApiKey
        {
            get { return ConfigurationManager.AppSettings["WeatherUnderGroundIOKey"]; }
        }

        #region Private Methods

        private List<Weather> ConvertForecastModel(ThreeDaysForecastModel model)
        {
            List<Weather> result = new List<Weather>();

            for (int i = 0; i < 4; i++)
            {
                Weather resultItem = new Weather();
                
                resultItem.Temperature = DataExtractor.GetAverageTemperature(model.Forecast.SimpleForecast.ForecastDay[i]);
                resultItem.Cloudness = DataExtractor.GetCloudness(model.Forecast.SimpleForecast.ForecastDay[i]);
                resultItem.Precipitation = DataExtractor.GetPrecipitation(model.Forecast.SimpleForecast.ForecastDay[i]);
                
                result.Add(resultItem);
            }
            
            return result;
        }
        
        private string GetLocationByLatLng(Location location)
        {

            using (var client = new WebClient())
            {
                string requestUrl = string.Format(@"http://api.wunderground.com/api/{0}/geolookup/q/{1},{2}.json", ApiKey, location.Latitude, location.Longitude);
                var json = client.DownloadString(requestUrl);
                var serializer = new JavaScriptSerializer();
                GeoLookupModel model = serializer.Deserialize<GeoLookupModel>(json);
                return model.Location.Tz_long;
            }
        }

        private ThreeDaysForecastModel ThreeDaysForecast(string locationUrl)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(@"http://api.wunderground.com/api/{0}/forecast/q/{1}.json", ApiKey, locationUrl);
                var json = client.DownloadString(requestUrl);
                var serializer = new JavaScriptSerializer();
                ThreeDaysForecastModel model = serializer.Deserialize<ThreeDaysForecastModel>(json);
                return model;
            }
        }


        #endregion
    }





}

