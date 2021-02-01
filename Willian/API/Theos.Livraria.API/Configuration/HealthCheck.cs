using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace Theos.Livraria.Api.Configuration
{
    public static class HealthCheck
    {

        public static IServiceCollection ConfigurationHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("ConnLivraria"));

            return services;
        }

    }
}