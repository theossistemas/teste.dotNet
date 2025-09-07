
using TheosLivraria.Domain.Interfaces.Repositories;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAsync(string email);
    }
}
