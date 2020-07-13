using System;
using System.Globalization;

namespace LibraryStore.Tests.Extensions
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        }

        public static DateTime ToDateTime(this string str)
        {
            return DateTime.Parse(str, CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}