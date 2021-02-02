using JeffersonMello.Livraria.Data.Context;
using JeffersonMello.Livraria.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Ninject.Modules;
using Serilog;
using System.Collections.Generic;
using System.IO;

namespace JeffersonMello.Livraria.API
{
    public class Startup
    {
        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion Public Properties

        #region Public Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Public Constructors

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region SeriLog

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\jeffersonmello.livraria.api.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            #endregion SeriLog

            #region Data Context

            var connection = Configuration["ConnectionStrings:SqlServerSandBox"];

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connection);

            Program.dataContext = new DataContext(optionsBuilder.Options);

            #endregion Data Context

            #region Ninject 

            Program.kernel = new Ninject.StandardKernel();
            Program.kernel.Load(new List<NinjectModule>() {
                new Bindings(Program.kernel, Program.dataContext),
            });

            #endregion Ninject

            #region Services

            #region Swagger 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Livraria - Desafio 1 Theos",
                    Version = "v1",
                    Description = "Projeto para o teste 1 Theos",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Jefferson Mello Olynyki",
                        Url = new System.Uri("https://github.com/jeffersonmello"),
                    }
                });


                string appName =
                    PlatformServices.Default.Application.ApplicationName;

                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{appName}.xml"));
            });

            #endregion Swagger


            services.AddControllers();

            #endregion Services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "API Livraria - Desafio 1 Theos");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }           

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

        #endregion Public Methods
    }
}