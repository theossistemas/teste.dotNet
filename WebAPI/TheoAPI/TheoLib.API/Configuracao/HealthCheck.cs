using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace TheoLib.API.Configuracao
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
