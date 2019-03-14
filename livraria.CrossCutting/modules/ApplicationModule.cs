using livraria.Application;
using livraria.Application.common;
using livraria.Application.interfaces;
using livraria.Application.interfaces.common;
using Microsoft.Extensions.DependencyInjection;

namespace livraria.CrossCutting.modules
{
    public static class ApplicationModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IApplication<>),typeof(Application<>));

            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<ILivroApplication, LivroApplication>();
            services.AddScoped<IAutorApplication, AutorApplication>();
            services.AddScoped<ICategoriaApplication, CategoriaApplication>();
            services.AddScoped<IPerfilApplication, PerfilApplication>();
        }
    }
}
