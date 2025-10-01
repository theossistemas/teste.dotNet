using Moq;
using System.Linq.Expressions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;
using Library.CrossCutting.Auth.Interface;
using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.CrossCutting.Auth.Models;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignIn;

public class SignInHandlerTests
{
    private readonly Mock<IRepository<Login>> _loginRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly SignInHandler _handler;

    public SignInHandlerTests()
    {
        _loginRepositoryMock = new Mock<IRepository<Login>>();
        _tokenServiceMock = new Mock<ITokenService>();
        _handler = new SignInHandler(_loginRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidUserAndPassword_ReturnsToken()
    {
        // Arrange
        var command = new SignInCommand { UserName = "user1", Password = "Password123" };
        var login = new Login("user1", "user1@email.com", EncrypterExtensions.HashPassword("Password123"));

        _loginRepositoryMock
            .Setup(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(login);

        _tokenServiceMock
            .Setup(x => x.ResponseAuth(It.IsAny<LoginModel>()))
            .Returns(new TokenModel { Token = "token-123" });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("token-123", result.Token);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsArgumentException()
    {
        // Arrange
        var command = new SignInCommand { UserName = "unknown", Password = "Password123" };

        _loginRepositoryMock
            .Setup(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Login)null!);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Contains("usuário (unknown)", ex.Message);
    }

    [Fact]
    public async Task Handle_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var command = new SignInCommand { UserName = "user1", Password = "WrongPassword" };
        var login = new Login("user1", "user1@email.com", EncrypterExtensions.HashPassword("Password123"));

        _loginRepositoryMock
            .Setup(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(login);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Equal("Senha inválida", ex.Message);
    }
}