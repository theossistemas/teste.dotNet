using Data.Repositories;
using Domain.Interfaces;
using Domain.Interfaces.Services.CategoriaQuarto;
using Domain.Interfaces.Services.Usuario;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService {
        public static void ConfigureDependenciesService (IServiceCollection serviceCollection) {            
            serviceCollection.AddTransient<IUsuarioService, UsuarioService>();
            serviceCollection.AddTransient<ILivroServices, LivroService>();
            serviceCollection.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));                        
        }
    }
}
