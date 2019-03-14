using Microsoft.Extensions.DependencyInjection;
using Theos.Challenge.Library.Application.Contracts.Services;
using Theos.Challenge.Library.Application.Services;

namespace Theos.Challenge.Library.DepInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationDI (this IServiceCollection services)
        {             
            services.AddScoped<IBookAppService, BookAppService>();
            
            return services;
        }
    }
}