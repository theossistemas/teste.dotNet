using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbAppContext context)
            : base(context, context.Users)
        { }

        public async Task<User> AutenticationAsync(string username, string password)
            => await dbSet.SingleOrDefaultAsync(itm => itm.Username.Equals(username) && itm.Password.Equals(password));

        public override async Task<IList<User>> FindAllAsync()
            => await dbSet.OrderBy(itm => itm.Fullname).ToListAsync();

        public async Task<bool> ExistsByUsernameAsync(string username)
            => await dbSet.AnyAsync(itm => itm.Username.Equals(username));
    }
}