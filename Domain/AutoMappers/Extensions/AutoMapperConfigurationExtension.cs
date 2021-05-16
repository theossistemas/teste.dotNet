using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain
{
    public static class AutoMapperConfigurationExtension
    {
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            var configMapper = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new AuthenticationProfile());
                configuration.AddProfile(new ResponsePageableProfile());
                configuration.AddProfile(new BookProfile());
            });

            services.AddSingleton(configMapper.CreateMapper());

            return services;
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource,
            TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}
