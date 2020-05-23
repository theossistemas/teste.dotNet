using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheosBookStore.Auth.Infra.Context;
using TheosBookStore.CrossCutting.Models;
using TheosBookStore.Stock.Infra.Context;


namespace TheosBookStore.CrossCutting.Extensions
{
    public static class DBContextsExtensions
    {
        public static string BuildConnectionString(this IConfiguration configuration)
        {
            var appSettings = GetAppSettings(configuration);
            var builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = appSettings.DBName;
            builder.DataSource = appSettings.DBHost;
            builder.IntegratedSecurity = string.IsNullOrWhiteSpace(appSettings.DBUsuario) &&
                string.IsNullOrWhiteSpace(appSettings.DBSenha);
            builder.UserID = appSettings.DBUsuario;
            builder.Password = appSettings.DBSenha;
            var connectionString = builder.ConnectionString;
            return connectionString;
        }

        private static AppSettings GetAppSettings(IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            appSettings.DBHost = configuration["DBHOST"] ?? configuration["DatabaseConfig:DatabaseHost"];
            appSettings.DBUsuario = configuration["USERID"] ?? configuration["DatabaseConfig:UserID"];
            appSettings.DBSenha = configuration["USERPASS"] ?? configuration["DatabaseConfig:UserPass"];
            appSettings.DBName = configuration["UDBNAME"] ?? configuration["DatabaseConfig:DatabaseName"];
            return appSettings;
        }

        public static IServiceCollection AddSqlServer(this IServiceCollection services,
            string migrationAssembly, string connectionString)
        {
            services.AddDbContext<TheosBookStoreStockDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, m =>
                        m.MigrationsAssembly(migrationAssembly))
            );

            services.AddDbContext<TheosBookStoreAuthDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, m =>
                        m.MigrationsAssembly(migrationAssembly))
            );

            return services;
        }

        public static IHost InitializeDataBase(this IHost host)
        {
            var serviceScopeFactory = host.Services;

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            TheosBookStoreStockDbContext stockDB = services
                .GetRequiredService<TheosBookStoreStockDbContext>();
            stockDB.Database.Migrate();

            TheosBookStoreAuthDbContext authDB = services
               .GetRequiredService<TheosBookStoreAuthDbContext>();
            authDB.Database.Migrate();

            return host;
        }
    }


}
