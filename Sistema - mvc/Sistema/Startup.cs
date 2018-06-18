using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;
using Sistema.Data;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Sistema
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddMvc();

         // Configurando o serviço de documentação do Swagger
         //services.AddSwaggerGen(c =>
         //{
         //   c.SwaggerDoc("v1",
         //       new Info
         //       {
         //          Title = "Gestão de Livrarias",
         //          Version = "v1",
         //          Description = "Exemplo de API REST criada com o ASP.NET Core",
         //          Contact = new Contact
         //          {
         //             Name = "Dyego Scofield Ferreia Furletti",
         //             Url = "https://www.linkedin.com/in/dyego-scofield/",
         //          }
         //       });

         //   string caminhoAplicacao =
         //       PlatformServices.Default.Application.ApplicationBasePath;
         //   string nomeAplicacao =
         //       PlatformServices.Default.Application.ApplicationName;
         //   string caminhoXmlDoc =
         //       Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

         //   c.IncludeXmlComments(caminhoXmlDoc);
         //});
 
         services.AddDbContext<SistemaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SistemaContext")));
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, SistemaContext context)
      {
         if (env.IsDevelopment())
         {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
         }

         app.UseStaticFiles();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
         DbInitializer.Initialize(context);

         //// Ativando middlewares para uso do Swagger
         //app.UseSwagger();
         //app.UseSwaggerUI(c =>
         //{
         //   c.SwaggerEndpoint("/swagger/v1/swagger.json",
         //       "Gestão de Livrarias");
         //});
      }
   }
}
