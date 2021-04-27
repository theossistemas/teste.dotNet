using System.Linq;
using Livraria.Domain.Security.Models;
using Livraria.Domain.Security.Interfaces;
using Livraria.Infra.Data.Security.Contex;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Security.Entities;

namespace Livraria.Infra.Data.Security.Repository
{
    public class UserRepository : SecurityBaseRepository<User>, IUserRepository
    {
        public UserRepository(SecurityDbContext context) : base(context){
            
        }

        public User GetUser(string login, string password){
            return  _dbSet.AsNoTracking().Where(e => e.Login == login).AsEnumerable().FirstOrDefault(el => el.Login == login && el.Password == password);            
        }
        public User GetLogin(string login){
            return  _dbSet.AsNoTracking().Where(e => e.Login == login).AsEnumerable().FirstOrDefault(el => el.Login == login);            
        }

        
    }
}