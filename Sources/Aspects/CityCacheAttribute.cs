using System;
using WeatherAggregator.Core.Aspects;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Sources.Aspects
{
    [Serializable]
    public class CityCacheAttribute : MethodCacheAttribute
    {
        public CityCacheAttribute()
            : base(int.MaxValue, typeof(ISource)) { }
    }
}
