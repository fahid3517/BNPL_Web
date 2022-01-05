using System;

namespace Project.Utilities
{
    public static class DateTimeUtility
    {
        public static DateTime DateTimeNowInPKTimeZone()
        {
            string strPkTimezone = "Pakistan Standard Time";
            TimeZoneInfo tzPakistan;
            try
            {
                tzPakistan = TimeZoneInfo.FindSystemTimeZoneById(strPkTimezone);
            }
            catch (TimeZoneNotFoundException)
            {
                return DateTime.UtcNow.AddHours(5); 
            }

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzPakistan);
        }

        public static DateTime DateTimeNowInPKTimeZone(DateTime timestamp)
        {
            string strPkTimezone = "Pakistan Standard Time";
            TimeZoneInfo tzPakistan;
            try
            {
                tzPakistan = TimeZoneInfo.FindSystemTimeZoneById(strPkTimezone);
            }
            catch (TimeZoneNotFoundException)
            {
                return timestamp;
            }

            return TimeZoneInfo.ConvertTimeFromUtc(timestamp.ToUniversalTime(), tzPakistan);
        }
    }
}