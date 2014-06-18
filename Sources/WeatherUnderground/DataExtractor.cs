using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.Sources.WeatherUnderground
{
    public static class DataExtractor
    {

        public static double GetAverageTemperature(ForecastDay forecastDay)
        {
            double result = (forecastDay.High.Celsius + forecastDay.Low.Celsius)/2;

            return result;
        }

        public static int GetCloudness(ForecastDay forecastDay)
        {
            return forecastDay.Pop;
        }

        public static Precipitation GetPrecipitation(ForecastDay forecastDay)
        {
            return ConvertPrecipitation(forecastDay.Conditions);
        }

        private static Precipitation ConvertPrecipitation(string conditions)
        {
            switch (conditions)
            {
                case "Heavy Drizzle":
                case "Light Drizzle":
                case "Heavy Rain":
                case "Light Rain":
                case "Heavy Rain Mist":
                case "Light Rain Mist":
                case "Heavy Rain Showers":
                case "Light Rain Showers":
                case "Heavy Thunderstorms and Rain":
                case "Light Thunderstorms and Rain":
                    return Precipitation.Rain;

                case "Heavy Freezing Drizzle":
                case "Light Freezing Drizzle":
                case "Heavy Freezing Rain":
                case "Light Freezing Rain":
                case "Heavy Hail":
                case "Light Hail":
                case "Heavy Hail Showers":
                case "Lignt Hail Showers":
                case "Heavy Thunderstorms with Hail":
                case "Light Thunderstorms with Hail":
                case "Heavy Thunderstorms with Small Hail":
                case "Light Thunderstorms with Small Hail":
                    return Precipitation.Hail;

                case "Heavy Snow":
                case "Light Snow":
                case "Heavy Snow Grains":
                case "Light Snow Grains":
                case "Heavy Ice Crystals":
                case "Light Ice Crystals":
                case "Heavy Ice Pellets":
                case "Light Ice Pellets":
                case "Heavy Low Drifting Snow":
                case "Light Low Drifting Snow":
                case "Heavy Thunderstorms and Snow":
                case "Light Thunderstorms and Snow":
                case "Heavy Thunderstorms and Ice Pellets":
                case "Light Thunderstorms and Ice Pellets":
                case "Heavy Snow Showers":
                case "Light Snow Showers":
                case "Heavy Snow Blowing Snow Mist":
                case "Light Snow Blowing Snow Mist":
                case "Small Hail":
                    return Precipitation.Snow;

                case "Heavy Mist":
                case "Light Mist":
                case "Heavy Fog":
                case "Light Fog":
                case "Heavy Fog Patches":
                case "Light Fog Patches":
                case "Heavy Smoke":
                case "Light Smoke":
                case "Heavy Volcanic Ash":
                case "Light Volcanic Ash":
                case "Heavy Widespread Dust":
                case "Light Widespread Dust":
                case "Heavy Sand":
                case "Light Sand":
                case "Heavy Haze":
                case "Light Haze":
                case "Heavy Spray":
                case "Light Spray":
                case "Heavy Dust Whirls":
                case "Light Dust Whirls":
                case "Heavy Sandstorm":
                case "Light Sandstorm":
                case "Heavy Low Drifting Widespread Dust":
                case "Light Low Drifting Widespread Dust":
                case "Heavy Low Drifting Sand":
                case "Light Low Drifting Sand":
                case "Heavy Blowing Snow":
                case "Light Blowing Snow":
                case "Heavy Blowing Widespread Dust":
                case "Light Blowing Widespread Dust":
                case "Heavy Blowing Sand":
                case "Light Blowing Sand":
                case "Heavy Ice Pellet Showers":
                case "Light Ice Pellet Showers":
                case "Heavy Small Hail Showers":
                case "Light Small Hail Showers":
                case "Heavy Thunderstorm":
                case "Light Thunderstorm":
                case "Heavy Freezing Fog":
                case "Light Freezing Fog":
                case "Patches of Fog":
                case "Shallow Fog":
                case "Partial Fog":
                case "Overcast":
                case "Clear":
                case "Partly Cloudy":
                case "Mostly Cloudy":
                case "Scattered Clouds":
                case "Squalls":
                case "Funnel Cloud":
                case "Unknown Precipitation":
                case "Unknown":
                default:
                    return Precipitation.None;
            }
        }

    
    }
}
