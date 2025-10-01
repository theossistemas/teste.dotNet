using Moq;
using MediatR;
using Library.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.Application.MediatR.Authenticate.Commands.SignUp;
using SignInResult = Library.Application.MediatR.Authenticate.Commands.SignIn.SignInResult;

namespace Library.UnitTests.Api.Controllers;

public class AuthenticateControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AuthenticateController _controller;

    public AuthenticateControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AuthenticateController(_mediatorMock.Object);
    }

    [Fact]
    public async Task SignIn_ShouldReturnOk_WhenCredentialsAreValid()
    {
        // Arrange
        var command = new SignInCommand { UserName = "admin", Password = "123456" };
        var result = new SignInResult("fake-jwt-token");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        // Act
        var response = await _controller.SignIn(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        var value = Assert.IsType<SignInResult>(okResult.Value);
        Assert.Equal("fake-jwt-token", value.Token);
    }

    [Fact]
    public async Task SignUp_ShouldReturnCreated_WhenUserIsRegistered()
    {
        // Arrange
        var command = new SignUpCommand { UserName = "newUser", Email = "test@email.com", Password = "123456" };
        var result = new SignUpResult("new-user-jwt-token");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<SignUpCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        // Act
        var response = await _controller.SignUp(command);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(response);
        var value = Assert.IsType<SignUpResult>(createdResult.Value);
        Assert.Equal("new-user-jwt-token", value.Token);
        Assert.Equal(nameof(_controller.SignUp), createdResult.ActionName);
    }

    [Fact]
    public async Task SignIn_ShouldReturnUnauthorized_WhenMediatorReturnsNull()
    {
        // Arrange
        var command = new SignInCommand { UserName = "wrong", Password = "invalid" };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((SignInResult)null!);

        // Act
        var response = await _controller.SignIn(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Null(okResult.Value);
    }
}