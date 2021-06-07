using LoggerService.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Config
{
    public static class ConfigLoggerService
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
