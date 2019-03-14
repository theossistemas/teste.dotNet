using Microsoft.Extensions.DependencyInjection;
using Theos.Challenge.Library.Model.Contracts.Services;
using Theos.Challenge.Library.Model.Services;

namespace Theos.Challenge.Library.DepInjection
{
    public static class DomainDependencyInjection
    {
        public static IServiceCollection AddDomainDI (this IServiceCollection services)
        {             
            services.AddScoped<IBookDomainService, BookDomainService>();                      
            return services;
        }
    }
}