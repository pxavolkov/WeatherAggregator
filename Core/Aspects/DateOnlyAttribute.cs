using System;
using PostSharp.Aspects;

namespace WeatherAggregator.Core.Aspects
{
    [Serializable]
    public class DateOnlyAttribute : LocationInterceptionAspect
    {
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            base.OnGetValue(args);
            args.Value = ((DateTime) args.Value).Date;
        }

        public override bool CompileTimeValidate(PostSharp.Reflection.LocationInfo locationInfo)
        {
            return locationInfo.PropertyInfo.PropertyType == typeof (DateTime);
        }
    }
}
