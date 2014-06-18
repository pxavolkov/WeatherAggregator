using System;

namespace WeatherAggegator.Helpers
{
    public static class DateTimeHelpers
    {
        public static DateTime FromTimeStamp(long timestamp)
        {
            var result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            result = result.AddSeconds(timestamp).ToLocalTime();
            return result;
        }
    }
}
