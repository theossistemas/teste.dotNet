using livraria.Domain.interfaces;
using livraria.Domain.interfaces.common;
using livraria.Repository;
using livraria.Repository.common;
using Microsoft.Extensions.DependencyInjection;

namespace livraria.CrossCutting.modules
{
    public static class RepositoryModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
        }
    }
}
