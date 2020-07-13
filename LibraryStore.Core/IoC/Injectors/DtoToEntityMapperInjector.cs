using LibraryStore.Core.IoC.Interfaces;
using LibraryStore.Core.Mapper.DtoToEntity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Injectors
{
    public class DtoToEntityMapperInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IBookDtoToEntityMapper, BookDtoToEntityMapper>();
            services.AddSingleton<IUserDtoToEntityMapper, UserDtoToEntityMapper>();
        }
    }
}