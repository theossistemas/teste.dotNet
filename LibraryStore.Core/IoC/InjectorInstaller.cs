using LibraryStore.Core.IoC.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LibraryStore.Core.IoC
{
    public class InjectorInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            var injectors = InjectorSetUpBase.LoadAssemblies<InjectorInstaller, IInjector>()
                                             .Where(x => !x.GetType().Equals(this.GetType()))
                                             .ToList();
            injectors.ForEach(injector => injector.RegisterServices(services));
        }
    }
}