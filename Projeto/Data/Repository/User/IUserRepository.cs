using Domain.Entity;

namespace Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string username, string password); 
    }
}
