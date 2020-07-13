using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.DataStorage.Repositories;
using LibraryStore.Core.Mapper.DtoToEntity.Inputs;
using LibraryStore.Core.Mapper.EntityToDto;
using System.Threading.Tasks;

namespace LibraryStore.Core.Business
{
    public class UserBusiness : BaseBusiness<User, UserDto, UserInputDto>, IUserBusiness
    {
        public UserBusiness(IUserRepository userRepository,
                            IUserEntityToDtoMapper userMapper,
                            IUserInputDtoToEntityMapper userInputMapper)
            : base(userRepository, userMapper, userInputMapper)
        { }

        public override async Task<UserDto> Create(UserInputDto dto)
        {
            if (await ((IUserRepository)repository).ExistsByUsernameAsync(dto.Username))
                return null;

            var entity = inputMapper.ToMap(dto);
            entity = await repository.CreateAsync(entity);

            return mapper.ToMap(entity);
        }
    }
}