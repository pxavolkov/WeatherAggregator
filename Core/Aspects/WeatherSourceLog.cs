using System;
using System.Reflection;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Aspects
{
    /// <summary>
    /// Logs methods of weather sources.
    /// </summary>
    [Serializable]
    public class WeatherSourceLog : MethodLog
    {
        public override bool CompileTimeValidate(MethodBase method)
        {
            return typeof (ISource).IsAssignableFrom(method.DeclaringType);
        }
    }
}
