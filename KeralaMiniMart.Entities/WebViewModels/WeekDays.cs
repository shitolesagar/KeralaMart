using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public static class WeekDays
    {
        public static List<string> AllWeekDays { get; } = new List<string>()
        {
            "Sunday", 
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
        };
    }
}
