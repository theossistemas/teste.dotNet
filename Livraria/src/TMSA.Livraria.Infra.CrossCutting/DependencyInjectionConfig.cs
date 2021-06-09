using Microsoft.Extensions.DependencyInjection;
using TMSA.Livraria.Domain.Interfaces;
using TMSA.Livraria.Domain.Notifications;
using TMSA.Livraria.Domain.Services;
using TMSA.Livraria.Infra.Data.Repository;

namespace TMSA.Livraria.Infra.CrossCutting
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILivroRepository, LivroRepository>();

            services.AddScoped<ILivroServices, LivroService>();

            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
