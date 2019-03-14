using livraria.Service;
using livraria.Service.common;
using livraria.Service.interfaces;
using livraria.Service.interfaces.common;
using Microsoft.Extensions.DependencyInjection;

namespace livraria.CrossCutting.modules
{
    public static class ServiceModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IService<>), typeof(Service<>));

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IPerfilService, PerfilService>();
        }
    }
}
