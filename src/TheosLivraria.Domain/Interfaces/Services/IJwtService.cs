using TheosLivraria.Domain.Models.Enums;

namespace TheosLivraria.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username, RolesEnum role);
    }
}
