using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Theos.Library.CrossCutting
{
    public static class EnvironmentProperties
    {
        public static string StringConnection = "Data Source=localhost;Initial Catalog=Library;Persist Security Info=True;User ID=sa;Password=theos@00";
        public static string SaveFilePath = "/app/wwwroot/";
        public static string HttpDomain = "http://localhost:8080";
        public static string FilePath = "/api/v1/files/";
        public static string FilePattern = "{0}";
        public static string FileAccessPath = $"{HttpDomain}{FilePath}{FilePattern}";

        public static int RequestLimit = 100;
        public static int SessionLifeTime = 10;

        public static List<string> FormatList = new List<string>{"jpg", "png"};
        
    }
}
