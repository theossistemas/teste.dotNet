using Livraria.Domain.Interfaces.Armazenadores;
using Livraria.Domain.Serviços.Armazenadores;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IArmazenadorDeLivro), typeof(ArmazenadorDeLivro));
        }
    }
}
