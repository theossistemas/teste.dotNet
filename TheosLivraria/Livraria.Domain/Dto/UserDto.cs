using Livraria.Domain.Entidades;

namespace Livraria.Domain.Dto
{
    public class UserDto
    {
        public string Username { get;  set; }
        public string Password { get;  set; }
        public string Role { get;  set; }
        public static UserDto ConverterParaDto(User user)
        {
            return new UserDto()
            {
                Password = user.Password,
                Role = user.Role,
                Username = user.Username
            };
        }

    }
}
