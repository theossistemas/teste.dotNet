using LibraryStore.Core.IoC.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace LibraryStore.Core.IoC
{
    public class InjectorConfigurator : IConfigurator
    {
        public void ConfigureAppBuilder(IApplicationBuilder app, IConfiguration configuration)
        {
            var injectors = InjectorSetUpBase.LoadAssemblies<InjectorConfigurator, IConfigurator>()
                                             .Where(x => !x.GetType().Equals(this.GetType()))
                                             .ToList();
            injectors.ForEach(injector => injector.ConfigureAppBuilder(app, configuration));
        }
    }
}