
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Domain.Interfaces.Repository
{
    public interface IUserRepository : IEntityBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAsync(string email);
    }
}
