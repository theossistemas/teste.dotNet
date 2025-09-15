using Livraria.Domain.Entities;

namespace Livraria.Application.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(string username, string password, string role);
    }
}
