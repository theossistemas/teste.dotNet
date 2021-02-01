using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;

namespace Theos.Livraria.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigurationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api Livraria",
                    Description = "Modelo de cadastro, alteração, exclusão, consulta e lista do livro",
                    Contact = new OpenApiContact
                    { 
                        Name = "WGM",
                        Email = "wllian.mira@gmail.com"
                    }
                });


                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT Header using the Bearer scheme (obter via Login).\nExemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseConfigurationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Api Livraria"); 
                c.DefaultModelsExpandDepth(-1);
            });
            return app;
        }
    }
}