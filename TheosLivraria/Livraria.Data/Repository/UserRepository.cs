using Livraria.Domain.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Data.Repository
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            var user1 = new User("admin", "admin", "manager");
            user1.DefinirId(1);
            var user2 = new User("user", "user", "user");
            user2.DefinirId(2);

            users.Add(user1);
            users.Add(user2);
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
