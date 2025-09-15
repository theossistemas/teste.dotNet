using System.Data;
using System.Data.SqlClient;

namespace Livraria.API.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("DefaultConnection"))
            );

            return services;
        }
    }
}
