

using Livraria.Domain.Security.Entities;
using Livraria.Domain.Security.Models;

namespace Livraria.Domain.Security.Interfaces
{
    public interface IUserRepository : ISecurityBaseRepository<User>
    {
        User GetUser(string login, string password);        
        User GetLogin(string login);        
    }
}