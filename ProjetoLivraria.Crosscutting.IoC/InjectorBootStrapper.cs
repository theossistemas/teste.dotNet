using Microsoft.Extensions.DependencyInjection;
using ProjetoLivraria.Application.Interfaces;
using ProjetoLivraria.Application.Services;
using ProjetoLivraria.Data;
using ProjetoLivraria.Data.Repositories;
using ProjetoLivraria.Domain.Repositories.Interfaces;

namespace ProjetoLivraria.Crosscutting.IoC
{
    public class InjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILivroAppService, LivroAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();

            services.AddTransient<ProjetoLivrariaContext>();
        }
    }
}
