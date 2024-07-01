using LivrariaJc.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gp.CrossCutting.Dependencies
{
    public static class DependenciesResolverData
    {
        public static IServiceCollection ResolveData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LivrariaJcContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
