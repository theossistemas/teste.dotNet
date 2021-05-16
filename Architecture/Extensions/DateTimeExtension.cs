using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public static class DateTimeExtension
    {
        public static DateTime GetFirstDayOfMonth(this string dateTime)
        {
            var date = DateTime.Parse(dateTime);

            return date.GetFirstDayOfMonth();
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(this string dateTime)
        {
            var date = DateTime.Parse(dateTime);
            
            return date.GetLastDayOfMonth();
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            var firstDay = dateTime.GetFirstDayOfMonth();

            return firstDay.AddMonths(1).AddDays(-1);
        }
    }
}
