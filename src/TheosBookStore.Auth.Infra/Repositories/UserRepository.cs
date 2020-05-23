using System.Linq;

using AutoMapper;

using TheosBookStore.Auth.Domain.Entities;
using TheosBookStore.Auth.Domain.Repositories;
using TheosBookStore.Auth.Infra.Context;
using TheosBookStore.Auth.Infra.Models;
using TheosBookStore.LibCommon.Repositories;

namespace TheosBookStore.Auth.Infra.Repositories
{
    public class UserRepository : BaseRepository<User, UserModel>, IUserRepository
    {
        public UserRepository(TheosBookStoreAuthDbContext dbContext, IMapper mapper)
          : base(dbContext, mapper) { }
        public User GetByEmail(string email)
        {
            var user = DbSet.Where(user => user.Email == email).FirstOrDefault();
            User userEntity = _mapper.Map<UserModel, User>(user);
            return userEntity;
        }
    }
}
