using JeffersonMello.Livraria.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ninject;

namespace JeffersonMello.Livraria.API
{
    public class Program
    {
        #region Public Fields

        public static StandardKernel kernel;

        public static DataContext dataContext;

        #endregion Public Fields

        #region Public Methods

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion Public Methods
    }
}