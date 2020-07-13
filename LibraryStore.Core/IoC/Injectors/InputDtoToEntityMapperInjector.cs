using LibraryStore.Core.IoC.Interfaces;
using LibraryStore.Core.Mapper.DtoToEntity.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Injectors
{
    public class InputDtoToEntityMapperInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IBookInputDtoToEntityMapper, BookInputDtoToEntityMapper>();
            services.AddSingleton<IUserInputDtoToEntityMapper, UserInputDtoToEntityMapper>();
        }
    }
}