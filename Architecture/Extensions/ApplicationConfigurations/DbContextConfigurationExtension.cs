using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public static class DbContextConfigurationExtension
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            services.AddDbContextPool<ApplicationDataContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("BookStoreConnectionString"), b => {
                    b.MigrationsAssembly("BookStore");
                    b.EnableRetryOnFailure();
                });
                option.EnableDetailedErrors();
                option.UseInternalServiceProvider(serviceProvider);
            }, 20);

            return services;
        }
    }
}
