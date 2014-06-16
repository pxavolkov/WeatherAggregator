using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace Sources
{
    public class SourceExample : ISource
    {
        private static string apiKey = "";
            

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

        public void Dispose()
        {

        }

        #region Private Methods

        public static void MakeRequest(string requestUrl)
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(string.Format(@"http://api.wunderground.com/api/{0}/forecast/q/CA/San_Francisco.json", apiKey));
                var serializer = new JavaScriptSerializer();
                SomeModel model = serializer.Deserialize<SomeModel>(json);
                // TODO: do something with the model
            }
        }

        private class SomeModel
        {
            public Response Response { get; set; }
            public Forecast Forecast { get; set; }
        }

        #endregion
    }

    public class Forecast
    {
        public Txt_forecast Txt_Forecast { get; set; }
        public SimpleForecast SimpleForecast { get; set; }
    }

    public class SimpleForecast
    {
        List<ForecastDay> ForecastDay { get; set; } 
    }

    public class ForecastDay
    {
        public ForecastDayDate Date { get; set; }
        public int Period { get; set; }
        public Temperature High { get; set; }
        public Temperature Type { get; set; }
    }

    public class ForecastDayDate
    {
        public int Epoch { get; set; }
        public string Pretty { get; set; }
        public short Day { get; set; }
        public short Month { get; set; }
        public short Year { get; set; }
        public short Yday { get; set; }
        public short Hour { get; set; }
        public short Min { get; set; }
        public short Sec { get; set; }
    }

    public class Temperature 
    {
        public int Fahrenheit { get; set; }
        public int Celsius { get; set; }
    }

    public class Txt_forecast
    {
        public DateTime Date { get; set; }
        public List<TxtForecastDay> ForecastDay { get; set; }
    }

    public class TxtForecastDay
    {
        public int Period { get; set; }
        public string Icon { get; set; }
        public string Icon_Url { get; set; }
        public string Title { get; set; }
        public string FctTest_Metric { get; set; }
        public int Pop { get; set; }

    }

    public class Response
    {
        public string Version { get; set; }
        public string TermsOfService { get; set; }
        public Features Features { get; set; }
    }

    public class Features
    {
        public int Forecast { get; set; }
    }
}

