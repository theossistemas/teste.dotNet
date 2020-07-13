using LibraryStore.Core.DataStorage.Repositories;
using LibraryStore.Core.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Injectors
{
    public class RepositoryInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}