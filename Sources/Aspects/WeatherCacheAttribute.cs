using System;
using System.Configuration;
using WeatherAggregator.Core.Aspects;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Sources.Aspects
{
    [Serializable]
    public class WeatherCacheAttribute : MethodCacheAttribute
    {
        public WeatherCacheAttribute()
            : base(typeof(ISource)) { }

        protected override int Timeout
        {
            get { return int.Parse(ConfigurationManager.AppSettings["WeatherCacheTimeoutSeconds"]); }
        }
    }
}
