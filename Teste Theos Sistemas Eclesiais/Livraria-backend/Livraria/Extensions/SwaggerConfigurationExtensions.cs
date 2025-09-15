using Microsoft.OpenApi.Models;

namespace Livraria.API.Extensions
{
    public static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Livraria",
                    Version = "v1",
                    Description = "API para gestão de livros e categorias"
                });
            });

            return services;
        }
    }
}
