using System;

using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using TheosBookStore.Auth.App.Services;
using TheosBookStore.Auth.App.Services.Impl;
using TheosBookStore.Auth.Domain.Repositories;
using TheosBookStore.Auth.Infra.Repositories;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Factories.Impl;
using TheosBookStore.Stock.App.Services;
using TheosBookStore.Stock.App.Services.Impl;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;
using TheosBookStore.Stock.Domain.Services.Impl;
using TheosBookStore.Stock.Infra.Mappers.Profiles;
using TheosBookStore.Stock.Infra.Repositories;
using TheosBookStore.Auth.Infra.Mapping.Profiles;

namespace TheosBookStore.CrossCutting.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            return services;
        }

        public static IServiceCollection AddStockDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IBookRegister, BookRegister>();
            services.AddScoped<IBookUpdater, BookUpdater>();
            services.AddScoped<IBookRemover, BookRemover>();
            services.AddScoped<IBookList, BookList>();
            services.AddTransient<IBookFactory, BookFactory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services,
            Type type)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<BookProfile>();
                config.AddProfile<PublisherProfile>();
                config.AddProfile<AuthorProfile>();
                config.AddProfile<UserProfile>();
            }, type);
            return services;
        }
    }
}
