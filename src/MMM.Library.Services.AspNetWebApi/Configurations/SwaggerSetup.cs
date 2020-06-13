using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace MMM.Library.Services.AspNetWebApi.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "V1 - teste.dotNet - Desafio 1",
                    Description = "Teste para vaga de .Net Developer - jun/2020",
                    Contact = new OpenApiContact { Name = "Márcio Molina Morassutti", Email = "marcio.molina.m@gmail.com", Url = new Uri("https://github.com/theossistemas/teste.dotNet") },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "V2 - teste.dotNet - Desafio 1",
                    Description = "Teste para vaga de .Net Developer - jun/2020",
                    Contact = new OpenApiContact { Name = "Márcio Molina Morassutti", Email = "marcio.molina.m@gmail.com", Url = new Uri("https://github.com/theossistemas/teste.dotNet") },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });



                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 - teste.dotNet - Desafio 1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 - teste.dotNet - Desafio 1");
            });
        }
    }
}
