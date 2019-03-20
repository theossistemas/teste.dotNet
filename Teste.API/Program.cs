using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Teste.API
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("hosting.json", optional: false)
                     .Build();

            IWebHostBuilder hostConfig = new WebHostBuilder();

            hostConfig = hostConfig.UseKestrel();

            hostConfig.UseConfiguration(config);

            hostConfig = hostConfig.UseUrls(config.GetValue<string>("server.urls"));

            hostConfig = hostConfig.UseStartup<Startup>();

            var host = hostConfig.Build();

            host.Run();
        }
    }
}
