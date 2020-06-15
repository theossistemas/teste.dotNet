using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MMM.Library.Services.AspNetWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureLogging(logging => 
                 {
                     logging.ClearProviders();
                     logging.AddConsole();
                     //logging.AddDebug();
                     //logging.AddEventLog();
                     //logging.AddEventSourceLogger();
                     //logging.AddTraceSource(sourceSwitchName);
                 })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                    webBuilder.UseStartup<Startup>();
                 });               
    }
}
