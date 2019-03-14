using livraria.CrossCutting.modules;
using Microsoft.Extensions.DependencyInjection;

namespace livraria.CrossCutting
{
    public class Bindings
    {
        public static void Start(IServiceCollection services)
        {
            RepositoryModule.Register(services);
            ServiceModule.Register(services);
            ApplicationModule.Register(services);
        }
    }
}
