using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Application.UseCases.Book.Update.DTOs;
using TheosLivraria.Application.UseCases.User.Auth;
using TheosLivraria.Application.UseCases.User.Auth.DTOs;
using TheosLivraria.Application.UseCases.User.Create;
using TheosLivraria.Application.UseCases.User.Create.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repositories;
using TheosLivraria.Domain.Interfaces.Repository;
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
        services.AddScoped<IUseCase<CreateUserInput, CreateUserOutput>, CreateUserUseCase>();
        services.AddScoped<IUseCase<CreateBookInput, CreateBookOutput>, CreateBookUseCase>();
        services.AddScoped<IUseCase<UpdateBookInput, UpdateBookOutput>, UpdateBookUseCase>();
        services.AddScoped<IUseCase<AuthUserInput, AuthUserOutput>, AuthUserUseCase>();


        services.AddScoped<IValidator<CreateUserInput>, CreateUserInputValidator>();
        services.AddScoped<IValidator<AuthUserInput>, AuthUserInputValidator>();
        services.AddScoped<IValidator<CreateBookInput>, CreateBookInputValidator>();
        services.AddScoped<IValidator<UpdateBookInput>, UpdateBookInputValidator>();
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
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IJwtService, JwtService>();
        // Registrar repositórios




        return services;
    }

    public static void ExecuteSeeds(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        SeedData.Initialize(db);
    }
}
