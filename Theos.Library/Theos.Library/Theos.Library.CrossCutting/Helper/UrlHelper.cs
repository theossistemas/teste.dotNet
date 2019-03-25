using System;
using System.Collections.Generic;
using System.Text;

namespace Theos.Library.CrossCutting.Helper
{
    public static class UrlHelper
    {
        public static string UrlDomainSimply(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            return url.Replace(EnvironmentProperties.HttpDomain, string.Empty);
        }

        public static string UrlFormatter(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            return url.StartsWith(EnvironmentProperties.FilePath) ? $"{EnvironmentProperties.HttpDomain}{url}" : url;
        }
    }
}
