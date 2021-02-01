
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; 
using Theos.Livraria.Application.Services;
using Theos.Livraria.Data.Repository;
using Theos.Livraria.Domain.Interface.Repository;
using Theos.Livraria.Domain.Interface.Services;

namespace Theos.Livraria.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigurationDependencyInjection(this IServiceCollection services)
        {
            //Service
            services.AddTransient<ILivroService, LivroService>();
            services.AddTransient<IUsuarioService, UsuarioService>();

            //Repository
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ILivroRepository, LivroRepository>();

            //Profile
            services.AddAutoMapper(typeof(LivroProfile));
            services.AddAutoMapper(typeof(UsuarioProfile));

            return services;
        }
    }
}