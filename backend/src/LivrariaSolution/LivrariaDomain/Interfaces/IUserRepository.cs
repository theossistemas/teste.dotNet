using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<int> SaveChangesAsync();
    }
}
