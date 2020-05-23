using Microsoft.Extensions.DependencyInjection;
using TheosBookStore.Web.Services;
using TheosBookStore.Web.Services.Impl;

namespace TheosBookStore.Web.Extensions
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
