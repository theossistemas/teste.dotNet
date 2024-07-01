using LivrariaJc.Domain.Interface.Repositories;
using Microsoft.Extensions.DependencyInjection;
using LivrariaJc.Service.Services;
using LivrariaJc.Domain.Interfaces.Repositories;
using LivrariaJc.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using LivrariaJc.Data.Repositories;
using LivrariaJc.Data.Repository;

namespace LivrariaJc.CrossCutting.Dependencies
{
    public static class DependenciesResolver
    {
        public static IServiceCollection ResolveInjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILivroServices, LivroServices>();
            services.AddScoped<IAutenticarServices, AutenticarServices>();

            services.AddScoped<ILivroRepositories, LivroRepositories>();

            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));

            return services;
        }
    }
}
