
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.INFRA.Context;
using TheosLivraria.INFRA.Repository;
using Xunit;

namespace TheosLivraria.Integration.Tests.Repository
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly TheosLivrariaDbContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TheosLivrariaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new TheosLivrariaDbContext(options);
            _repository = new UserRepository(_context);

            SeedData().Wait();
        }

        private async Task SeedData()
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Name = "Admin1", Email = "admin1@test.com", PasswordHash = "hash1", Active = true, Role = new RolesEntity { Name = "Admin" } },
                new UserEntity { Name = "User1", Email = "user1@test.com", PasswordHash = "hash2", Active = true, Role = new RolesEntity { Name = "User" } },
                new UserEntity { Name = "InactiveUser", Email = "inactive@test.com", PasswordHash = "hash3", Active = false, Role = new RolesEntity { Name = "User" } },
            };

            await _context.User.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddAsync_Should_AddUser()
        {
            var user = new UserEntity
            {
                Name = "New User",
                Email = "newuser@test.com",
                PasswordHash = "hash4",
                Active = true,
                Role = new RolesEntity { Name = "User" }
            };

            await _repository.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("New User", result!.Name);
        }

        [Fact]
        public async Task Update_Should_UpdateUser()
        {
            var user = (await _repository.GetAllAsync(x => x.Active)).First();
            user.Name = "Updated Name";

            _repository.Update(user);
            await _context.SaveChangesAsync();

            var updatedUser = await _repository.GetByIdAsync(user.Id);
            Assert.Equal("Updated Name", updatedUser!.Name);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnActiveUsers()
        {
            var users = await _repository.GetAllAsync(x => x.Active);
            Assert.All(users, u => Assert.True(u.Active));
            Assert.Equal(2, users.ToList().Count); // somente ativos
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCorrectUser()
        {
            var user = (await _repository.GetAllAsync(x => x.Active)).First();
            var result = await _repository.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result!.Id);
        }

        [Fact]
        public async Task GetByEmailAsync_Should_ReturnActiveUserWithRole()
        {
            var email = "admin1@test.com";
            var user = await _repository.GetByEmailAsync(email);

            Assert.NotNull(user);
            Assert.Equal(email, user!.Email);
            Assert.NotNull(user.Role);
            Assert.Equal("Admin", user.Role!.Name);
        }

        [Fact]
        public async Task GetByEmailAsync_Should_ReturnNullForInactiveUser()
        {
            var email = "inactive@test.com";
            var user = await _repository.GetByEmailAsync(email);

            Assert.Null(user); 
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
