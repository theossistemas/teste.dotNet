using Livraria.Data.Repository;
using Livraria.Domain.Interfaces.Armazenadores;
using Livraria.Domain.Interfaces.Repository;
using Livraria.Domain.Serviços.Armazenadores;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IArmazenadorDeLivro), typeof(ArmazenadorDeLivro));
            services.AddScoped(typeof(ILivroRepositorio), typeof(LivroRepositorio));
            services.AddScoped(typeof(IAutorRepositorio), typeof(AutorRepositorio));
        }
    }
}
