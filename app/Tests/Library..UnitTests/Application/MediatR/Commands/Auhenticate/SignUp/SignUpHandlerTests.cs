using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.CrossCutting.Auth.Interface;
using Library.CrossCutting.Auth.Models;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;
using Moq;
using System.Linq.Expressions;

namespace Library.UnitTests.Application.MediatR.Commands.Auhenticate.SignUp;

public class SignUpHandlerTests
{
    private readonly Mock<IRepository<Login>> _loginRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly SignUpHandler _handler;

    public SignUpHandlerTests()
    {
        _loginRepositoryMock = new Mock<IRepository<Login>>();
        _tokenServiceMock = new Mock<ITokenService>();
        _handler = new SignUpHandler(_loginRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsToken()
    {
        // Arrange
        var command = new SignUpCommand { UserName = "newuser", Email = "newuser@email.com", Password = "Password123" };

        _loginRepositoryMock.Setup(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Login)null!);

        _loginRepositoryMock.Setup(x => x.Insert(It.IsAny<Login>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(command);

        _tokenServiceMock.Setup(x => x.ResponseAuth(It.IsAny<LoginModel>()))
            .Returns(new TokenModel { Token = "token-123" });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("token-123", result.Token);
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsArgumentException()
    {
        // Arrange
        var command = new SignUpCommand { UserName = "existinguser", Email = "email@email.com", Password = "Password123" };
        var existingUser = new Login("existinguser", "email@email.com", EncrypterExtensions.HashPassword("Password123"));

        _loginRepositoryMock.Setup(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Contains("usuário (existinguser)", ex.Message);
    }

    [Fact]
    public async Task Handle_EmailAlreadyExists_ThrowsArgumentException()
    {
        // Arrange
        var command = new SignUpCommand { UserName = "newuser", Email = "existing@email.com", Password = "Password123" };
        var existingEmailUser = new Login("existinguser", "email@email.com", EncrypterExtensions.HashPassword("Password123"));

        _loginRepositoryMock.SetupSequence(x => x.FindOne(It.IsAny<Expression<Func<Login, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Login)null!)          // User check
            .ReturnsAsync(existingEmailUser);   // Email check

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Contains("Email (existing@email.com)", ex.Message);
    }
}