using System;
using WeatherAggregator.Core.Aspects;

namespace WeatherAggregator.Sources.Aspects
{
    [Serializable]
    public class CityCacheAttribute : MethodCacheAttribute
    {
        public CityCacheAttribute()
            : base(null) { }

        protected override int Timeout
        {
            get { return int.MaxValue; }
        }
    }
}
