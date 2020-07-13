using LibraryStore.Core.IoC.Interfaces;
using LibraryStore.Core.Mapper.EntityToDto;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Injectors
{
    public class EntityToDtoMapperInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IBookEntityToDtoMapper, BookEntityToDtoMapper>();
            services.AddSingleton<IUserEntityToDtoMapper, UserEntityToDtoMapper>();
        }
    }
}