using System;
using WeatherAggregator.Core.Aspects;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Sources.Aspects
{
    [Serializable]
    public class WeatherCacheAttribute : MethodCacheAttribute
    {
        public static int TimeoutInSeconds;

        public WeatherCacheAttribute()
            : base(TimeoutInSeconds, typeof(ISource)) { }
    }
}
