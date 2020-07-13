using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mappers;

namespace LibraryStore.Core.Mapper.DtoToEntity
{
    public class UserDtoToEntityMapper : BaseMapper<UserDto, User>, IUserDtoToEntityMapper
    { }
}