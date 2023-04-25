using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Service.ExtensionMethods
{
    public static class DateExtension
    {
        public static string ToStringDatePattern(this DateTime value)
        {
            return value.ToString("dd MMM yyyy");
        }

        public static string ToStringDateTimePattern(this DateTime value)
        {
            return value.ToString("dd MMM yyyy hh:mm tt");
        }

        public static string ToStringDayOfWeekPattern(this DateTime value)
        {
            return value.ToString("dddd, dd MMM yyyy, hh:mm tt");
        }

        public static string ToStringShortDayOfWeekPattern(this DateTime value)
        {
            return value.ToString("dd MMM yyyy (ddd)");
        }

        public static long ToTimeStamp(this DateTime value)
        {
            return ((value.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }
    }
}
