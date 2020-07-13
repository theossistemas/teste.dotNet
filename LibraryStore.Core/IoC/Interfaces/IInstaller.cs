using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Interfaces
{
    public interface IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}