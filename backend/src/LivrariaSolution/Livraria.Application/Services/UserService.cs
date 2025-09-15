using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Livraria.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;
            var hash = HashPassword(password);
            return user.PasswordHash == hash ? user : null;
        }

        public async Task<User> RegisterAsync(string username, string password, string role)
        {
            var existing = await _userRepository.GetByUsernameAsync(username);
            if (existing != null)
                throw new System.InvalidOperationException("Username já existe.");
            var user = new User
            {
                Id = System.Guid.NewGuid(),
                Username = username,
                PasswordHash = HashPassword(password),
                Role = role
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("Usuário registrado: {Username}", username);
            return user;
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return System.Convert.ToBase64String(bytes);
        }
    }
}
