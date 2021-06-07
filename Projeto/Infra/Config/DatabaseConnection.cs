using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Config
{
    public static class DatabaseConnection
    {
        public static void ConnectSqlServer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"), action => action.MigrationsAssembly("Data"));
            });
        }
    }
}
