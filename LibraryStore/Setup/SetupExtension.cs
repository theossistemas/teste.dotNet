using LibraryStore.Core.IoC;
using LibraryStore.Core.IoC.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryStore.Setup
{
    public static class SetUpExtension
    {
        public static void InstallAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            var installers = LoadAssemblyInstaller<InjectorInstaller, IInstaller>()
                             .Concat(LoadAssemblyInstaller<RESTInstaller, IInstaller>())
                             .ToList();

            installers.ForEach(installer => installer.InstallerServices(services, configuration));
        }

        public static void ConfigureAppBuilder(this IApplicationBuilder app, IConfiguration configuration)
        {
            var configurators = LoadAssemblyInstaller<InjectorConfigurator, IConfigurator>()
                             .Concat(LoadAssemblyInstaller<RESTConfigurator, IConfigurator>())
                             .ToList();

            configurators.ForEach(configurator => configurator.ConfigureAppBuilder(app, configuration));
        }

        private static IEnumerable<U> LoadAssemblyInstaller<T, U>() where T : U
        {
            return typeof(T).Assembly
                            .ExportedTypes
                            .Where(x => typeof(U).IsAssignableFrom(x)
                                     && !x.IsInterface
                                     && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<U>();
        }
    }
}