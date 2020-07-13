using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.DataStorage.Repositories;
using LibraryStore.Core.Mapper.EntityToDto;
using LibraryStore.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LibraryStore.Core.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly IUserEntityToDtoMapper userMapper;

        public AccountBusiness(IConfiguration configuration,
                               IUserRepository userRepository,
                               IUserEntityToDtoMapper userMapper)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userMapper = userMapper;
        }

        public async Task<dynamic> Authenticate(LoginInputDto dto)
        {
            var user = await userRepository.AutenticationAsync(dto.Username, dto.Password);
            if (user == null)
                return null;

            var token = TokenService.GenerateToken(user, configuration.GetValue<string>("Secret"));
            return new
            {
                user = userMapper.ToMap(user),
                token
            };
        }
    }
}