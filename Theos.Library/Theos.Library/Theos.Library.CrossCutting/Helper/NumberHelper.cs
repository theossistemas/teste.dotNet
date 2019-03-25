using System.Text.RegularExpressions;

namespace Theos.Library.CrossCutting.Helper
{
    public static class NumberHelper
    {
        public static string OnlyNumbers(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var regexObj = new Regex(@"[^\d]");
            return regexObj.Replace(value.Trim(), string.Empty);
        }
    }
}
