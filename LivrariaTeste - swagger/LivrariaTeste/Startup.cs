using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LivrariaTeste.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Sistema.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace LivrariaTeste
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
         var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
         Configuration = builder.Build();
      }

      public IConfigurationRoot Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

         services.AddDbContext<LivrariaTesteContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("LivrariaTesteContext")));

         // Configurando o serviço de documentação do Swagger
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1",
                new Info
                {
                   Title = "Gestor de Livrarias",
                   Version = "v1",
                   Contact = new Contact
                   {
                      Name = "Dyego Scofield Ferreira Furletti",
                      Url = "https://www.linkedin.com/in/dyego-scofield/"
                   }
                });

            string caminhoAplicacao =
                PlatformServices.Default.Application.ApplicationBasePath;
            string nomeAplicacao =
                PlatformServices.Default.Application.ApplicationName;
            string caminhoXmlDoc =
                Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

            c.IncludeXmlComments(caminhoXmlDoc);
         });
      }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LivrariaTesteContext context)
        {
         loggerFactory.AddConsole(Configuration.GetSection("Logging"));
         loggerFactory.AddDebug();

         app.UseMvc();

         DbInitializer.Initialize(context);

         // Ativando middlewares para uso do Swagger
         app.UseSwagger();
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Conversor de Temperaturas");
         });
      }
    }
}
