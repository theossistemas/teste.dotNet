
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheosLivraria.Application.Logs;
using TheosLivraria.Domain.Interfaces.Jwt;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.INFRA.Context;
using TheosLivraria.INFRA.Repository;
using TheosLivraria.INFRA.Services.Jwt;

namespace TheosLivraria.INFRA
{
    public static class ExtensionInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
             services.AddScoped<IAuditService, AuditService>();
             services.AddScoped<IJwtService, JwtService>();
            #endregion

            #region Repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(TheosLivraria.Domain.Interfaces.Repository.IEntityBaseRepository<>), typeof(TheosLivraria.INFRA.Repository.EntityBaseRepository<>));
            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            #endregion

            ConfigureDatabase(services, configuration);
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TheosLivrariaDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
