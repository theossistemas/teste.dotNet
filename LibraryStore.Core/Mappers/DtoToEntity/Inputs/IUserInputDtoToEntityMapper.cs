using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mappers;

namespace LibraryStore.Core.Mapper.DtoToEntity.Inputs
{
    public interface IUserInputDtoToEntityMapper : IMapper<UserInputDto, User>
    { }
}