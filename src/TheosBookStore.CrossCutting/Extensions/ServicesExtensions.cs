using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Factories.Impl;
using TheosBookStore.Stock.App.Services;
using TheosBookStore.Stock.App.Services.Impl;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;
using TheosBookStore.Stock.Domain.Services.Impl;
using TheosBookStore.Stock.Infra.Mappers.Profiles;
using TheosBookStore.Stock.Infra.Repositories;

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
            services.AddTransient<IBookFactory, BookFactory>();
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
            }, type);
            return services;
        }
    }
}
