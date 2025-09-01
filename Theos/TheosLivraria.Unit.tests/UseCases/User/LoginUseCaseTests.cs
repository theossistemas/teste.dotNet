
using AutoBogus;
using Moq;
using TheosLivraria.Application.UseCases.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.GetLogin;
using TheosLivraria.Domain.Interfaces.Jwt;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Unit.Tests.UseCases.User
{
    public class LoginUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly LoginUseCase _sut;

        public LoginUseCaseTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _domainNotificationMock = new Mock<IDomainNotification>();

            _sut = new LoginUseCase(
                _userRepositoryMock.Object,
                _jwtServiceMock.Object,
                _domainNotificationMock.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var password = "123456";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new AutoFaker<UserEntity>()
                .RuleFor(u => u.Email, f => "user@test.com")
                .RuleFor(u => u.PasswordHash, f => hashedPassword)
                .RuleFor(u => u.Active, f => true)
                .RuleFor(u => u.Role, f => new Domain.Models.Entities.RolesEntity { Name = "Admin" })
                .Generate();

            var request = new RequestLoginViewModel
            {
                Email = user.Email,
                Password = password
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync(user);

            _jwtServiceMock.Setup(j => j.GenerateToken(user))
                           .Returns("token123");

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("token123", result.Token);
            Assert.Equal(user.Name, result.Username);
            Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new RequestLoginViewModel
            {
                Email = "notfound@test.com",
                Password = "123456"
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
            _domainNotificationMock.Verify(d => d.AddNotification("Validate", "Usuário ou senha inválidos."), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnNull_WhenUserIsInactive()
        {
            // Arrange
            var user = new AutoFaker<UserEntity>()
                .RuleFor(u => u.Active, f => false)
                .Generate();

            var request = new RequestLoginViewModel
            {
                Email = user.Email,
                Password = "123456"
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync(user);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
            _domainNotificationMock.Verify(d => d.AddNotification("Validate", "Usuário ou senha inválidos."), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ReturnNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new AutoFaker<UserEntity>()
                .RuleFor(u => u.PasswordHash, f => BCrypt.Net.BCrypt.HashPassword("correct_password"))
                .RuleFor(u => u.Active, f => true)
                .Generate();

            var request = new RequestLoginViewModel
            {
                Email = user.Email,
                Password = "wrong_password"
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync(user);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
            _domainNotificationMock.Verify(d => d.AddNotification("Validate", "Usuário ou senha inválidos."), Times.Once);
        }
    }
}
