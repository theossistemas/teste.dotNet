using LC.Domain;
using LC.Infrastruture.Repositories.Contracts;
using LC.Persistence;
using System.Linq;

namespace LC.Infrastruture.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserLogin(string login, string password)
        {
            return _context.Set<User>().Where(c => c.Login == login && c.AcessKey == password).FirstOrDefault();
        }
    }
}
