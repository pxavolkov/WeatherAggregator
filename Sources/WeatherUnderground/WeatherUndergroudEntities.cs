using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Sources.WeatherUnderground
{

    #region 3 Days Forecast
    //Model is not complete. Only some fields. JSON contains much more fields, but they are dont needed

    public class ThreeDaysForecastModel
    {
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        public SimpleForecast SimpleForecast { get; set; }
    }

    public class SimpleForecast
    {
        public List<ForecastDay> ForecastDay { get; set; }
    }

    public class ForecastDay
    {
        public ForecastDayDate Date { get; set; }
        public int Period { get; set; }
        public Temperature High { get; set; }
        public Temperature Low { get; set; }
        public string Conditions { get; set; }
        public int Pop { get; set; }

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

    #endregion

    #region GeoLookup
    //Model is not complete. Only some fields. JSON contains much more fields, but they are dont needed

    public class GeoLookupModel
    {
        public GeoLookupLocation Location { get; set; }
    }

    public class GeoLookupLocation
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Country_iso3166 { get; set; }
        public string Country_Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Tz_short { get; set; }
        public string Tz_long { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int Zip { get; set; }
        public int Magic { get; set; }
        public int Wmo { get; set; }
        public string L { get; set; }
        public string Requesturl { get; set; }
        public string Wuiurl { get; set; }
    }
    
    #endregion

}
