using LC.Domain;
using LC.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Infrastruture.Repositories.Contracts
{
    public interface IUserRepository : IRepositoryGeneric<User>
    {
        User GetUserLogin(string login, string password);
    }
}
