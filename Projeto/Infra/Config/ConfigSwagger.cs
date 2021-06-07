using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Infra.Config
{
    public static class ConfigSwagger
    {
        public static void ConfigureSwaggerGen(this IServiceCollection service)
        {
            service.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Desafio Técnico Theós API",
                    Description = "Uma API simples com operações de CRUD, autenticação e configuração do para demonstração de conhecimentos técnicos sobre as linguagens e ferramentas apresentadas no desafio.",
                    Contact = new OpenApiContact
                    {
                        Name = "Teo Nakati",
                        Email = "teonakati@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/teo-nakati/"),
                    }
                });

                var xmlFile = "SwaggerDocumentationConfig.xml";
                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                config.IncludeXmlComments(xmlFilePath);
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                config.RoutePrefix = string.Empty;
            });
        }
    }
}
