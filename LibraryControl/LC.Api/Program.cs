using System.IO;
using LB.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace LC.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.SetBasePath(Directory.GetCurrentDirectory());
                 config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                 config.AddEnvironmentVariables();
             }).UseDefaultServiceProvider(options => options.ValidateScopes = false).UseStartup<Startup>();
    }
}
