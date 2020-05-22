using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TheosBookStore.Web.Extensions
{
    public static class SwaggerConfigExtension
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            var info = new OpenApiInfo
            {
                Title = "Theós Book Store",
                Version = "v1",
                Description = "Test for .Net position",
                Contact = new OpenApiContact
                {
                    Name = "Francisco Thiago de Almeida",
                    Url = new Uri("https://www.blogdoft.com.br")
                }
            };
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(info.Version, info);
            });
            return services;
        }
    }
}
