using System;
using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

using TheosBookStore.CrossCutting.Extensions;
using TheosBookStore.Web.Lib;

namespace TheosBookStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogConfig();
            try
            {
                Log.Debug("Starting application");
                CreateHostBuilder(args)
                    .Build()
                    .InitializeDataBase()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to star the {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        private static void LogConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = AddConfigurationSettings(new ConfigurationBuilder())
                .Build();
            var log = new LogConfigBuilder(configuration, environment);
            log.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    AddConfigurationSettings(configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        public static IConfigurationBuilder AddConfigurationSettings(IConfigurationBuilder configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            configuration
                .AddEnvironmentVariables(prefix: "TBS_")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true);
            return configuration;
        }
    }
}
