
using TheosLivraria.Domain.Models.Enums;

namespace TheosLivraria.Domain.Models.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RolesEnum Role { get; set; }
    }
}
