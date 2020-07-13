using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Interfaces
{
    public interface IInjector
    {
        void RegisterServices(IServiceCollection services);
    }
}