using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Hosting; 
using Serilog;
using System;

namespace TheoLib.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminou inesperadamente.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog(dispose: true)
                .UseStartup<Startup>();
    }
}
