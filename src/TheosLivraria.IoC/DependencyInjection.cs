using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repositories;
using TheosLivraria.Domain.Interfaces.Services;
using TheosLivraria.Infrastructure.Data;
using TheosLivraria.Infrastructure.Data.Repositories;
using TheosLivraria.Infrastructure.Services;
using TheosLivraria.Infrastructure.UnitOfWork;


namespace TheosLivraria.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar use cases

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("TheosLivraria.Infrastructure")
            ));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IJwtService, JwtService>();
        // Registrar repositórios




        return services;
    }
}
