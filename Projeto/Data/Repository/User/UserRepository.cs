using Data.DatabaseContext;
using Domain.Entity;
using System.Linq;

namespace Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public User GetUser(string username, string password)
        {
            return Get(u => u.Username.Equals(username) && u.Password.Equals(password))
                .FirstOrDefault();
        }
    }
}
