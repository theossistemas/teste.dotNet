using Data.Repository.Wrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Config
{
    public static class ConfigRepositoryWrapper
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
