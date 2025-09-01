
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheosLivraria.API.Controllers;
using TheosLivraria.Application.UseCases.User.CreateAdmin;
using TheosLivraria.Application.UseCases.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.Login;
using Xunit;

namespace TheosLivraria.Integration.Tests.Controller
{
    public class AuthControllerTests
    {
        private readonly Mock<ILoginUseCase> _loginUseCaseMock;
        private readonly Mock<ICreateAdminUserCase> _createAdminUseCaseMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _loginUseCaseMock = new Mock<ILoginUseCase>();
            _createAdminUseCaseMock = new Mock<ICreateAdminUserCase>();
            _controller = new AuthController();
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginSucceeds()
        {
            // Arrange
            var request = new RequestLoginViewModel { Email = "teste@gmail.com", Password = "123" };
            var response = new ResponseLoginViewModel { Token = "jwt-token", Username = "Test", Role = "Admin" };

            _loginUseCaseMock.Setup(x => x.ExecuteAsync(request))
                             .ReturnsAsync(response);

            // Act
            var result = await _controller.Login(_loginUseCaseMock.Object, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<ResponseLoginViewModel>(okResult.Value);

            Assert.Equal(response.Token, returnedValue.Token);
            Assert.Equal(response.Username, returnedValue.Username);
            Assert.Equal(response.Role, returnedValue.Role);

            _loginUseCaseMock.Verify(x => x.ExecuteAsync(request), Times.Once);
        }

        [Fact]
        public async Task CreateAdmin_ShouldReturnCreated_WhenAdminCreated()
        {
            // Arrange
            var request = new RequestCreateAdminViewModel { Email = "admin@test.com", Password = "123", Username = "Admin" };
            var response = new ResponseCreateAdminViewModel { Id = 1 };

            _createAdminUseCaseMock.Setup(x => x.ExecuteAsync(request))
                                   .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAdmin(_createAdminUseCaseMock.Object, request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var returnedValue = Assert.IsType<ResponseCreateAdminViewModel>(createdResult.Value);

            Assert.Equal(response.Id, returnedValue.Id);

            _createAdminUseCaseMock.Verify(x => x.ExecuteAsync(request), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnOkWithNull_WhenLoginFails()
        {
            // Arrange
            var request = new RequestLoginViewModel { Email = "fail@test.com", Password = "123" };
            _loginUseCaseMock.Setup(x => x.ExecuteAsync(request))
                             .ReturnsAsync((ResponseLoginViewModel?)null);

            // Act
            var result = await _controller.Login(_loginUseCaseMock.Object, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);

            _loginUseCaseMock.Verify(x => x.ExecuteAsync(request), Times.Once);
        }

        [Fact]
        public async Task CreateAdmin_ShouldReturnCreatedWithNull_WhenValidationFails()
        {
            // Arrange
            var request = new RequestCreateAdminViewModel { Email = "fail@test.com", Password = "123", Username = "Admin" };
            _createAdminUseCaseMock.Setup(x => x.ExecuteAsync(request))
                                   .ReturnsAsync((ResponseCreateAdminViewModel?)null);

            // Act
            var result = await _controller.CreateAdmin(_createAdminUseCaseMock.Object, request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Null(createdResult.Value);

            _createAdminUseCaseMock.Verify(x => x.ExecuteAsync(request), Times.Once);
        }
    }
}
