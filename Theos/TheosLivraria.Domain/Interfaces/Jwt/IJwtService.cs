
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Domain.Interfaces.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity user);
    }
}
