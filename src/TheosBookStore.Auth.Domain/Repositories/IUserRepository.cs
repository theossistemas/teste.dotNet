using TheosBookStore.Auth.Domain.Entities;

namespace TheosBookStore.Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
    }
}
