using FluentValidation;
using FluentValidation.Results;
using Moq;
using TheosLivraria.Application.UseCases.User.Auth;
using TheosLivraria.Application.UseCases.User.Auth.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.Services;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Enums;
using Xunit;

namespace TheosLivraria.Tests.Application.UseCases.User.Auth
{
    public class AuthUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IValidator<AuthUserInput>> _validatorMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();

        private AuthUserUseCase CreateUseCase() =>
            new(_jwtServiceMock.Object, _validatorMock.Object, _userRepositoryMock.Object, _unitOfWorkMock.Object);

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenValidationFails()
        {
            // Arrange
            var input = new AuthUserInput { Email = "a", Password = "b" };
            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Email", "Erro") }));

            var useCase = CreateUseCase();

            var result = await useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.NotNull(result.MetaData);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenUserNotFound()
        {
            var input = new AuthUserInput { Email = "user@test.com", Password = "123" };
            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(input.Email))
                .ReturnsAsync((UserEntity?)null);

            var useCase = CreateUseCase();

            var result = await useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Usuário ou senha inválidos.", result.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenPasswordIncorrect()
        {
            var input = new AuthUserInput { Email = "user@test.com", Password = "wrong" };
            var user = new UserEntity { Id = Guid.NewGuid(), Name = "User", Email = input.Email, Password = BCrypt.Net.BCrypt.HashPassword("123"), IsActive = true, Role = RolesEnum.User };

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(input.Email))
                .ReturnsAsync(user);

            var useCase = CreateUseCase();

            var result = await useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Usuário ou senha inválidos.", result.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            var input = new AuthUserInput { Email = "user@test.com", Password = "123" };
            var user = new UserEntity { Id = Guid.NewGuid(), Name = "User", Email = input.Email, Password = BCrypt.Net.BCrypt.HashPassword("123"), IsActive = true, Role = RolesEnum.User };
            var token = "jwt-token";

            _validatorMock.Setup(v => v.ValidateAsync(input, default))
                .ReturnsAsync(new ValidationResult());
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(input.Email))
                .ReturnsAsync(user);
            _jwtServiceMock.Setup(j => j.GenerateToken(user.Id, user.Name, user.Role))
                .Returns(token);

            var useCase = CreateUseCase();

            var result = await useCase.ExecuteAsync(input);

            Assert.True(result.Success);
            Assert.Equal(user.Name, result.Value!.Name);
            Assert.Equal(user.Role.ToString(), result.Value.Role);
            Assert.Equal(token, result.Value.Token);
        }
    }
}
