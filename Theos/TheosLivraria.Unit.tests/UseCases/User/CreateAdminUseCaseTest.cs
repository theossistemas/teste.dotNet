
using AutoBogus;
using Microsoft.Extensions.Logging;
using Moq;
using TheosLivraria.Application.UseCases.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Unit.Tests.UseCases.User
{
    public class CreateAdminUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<ILogger<CreateAdminUseCase>> _loggerMock;
        private readonly CreateAdminUseCase _sut;

        public CreateAdminUseCaseTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _loggerMock = new Mock<ILogger<CreateAdminUseCase>>();

            _sut = new CreateAdminUseCase(
                _userRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _domainNotificationMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_Should_CreateAdmin_WhenValid()
        {
            // Arrange
            var request = new AutoFaker<RequestCreateAdminViewModel>().Generate();
            request.Email = "teste@gmail.com";

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            _userRepositoryMock.Verify(r => r.AddAsync(It.Is<UserEntity>(u =>
                u.Email == request.Email &&
                u.RoleId == (int)Domain.Models.Enum.RolesEnum.Admin
            )), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExecuteAsync_Should_NotCreateAdmin_WhenEmailExists()
        {
            // Arrange
            var request = new AutoFaker<RequestCreateAdminViewModel>().Generate();
            var existingUser = new AutoFaker<UserEntity>().Generate();

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync(existingUser);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<UserEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);

            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_Should_NotCreateAdmin_WhenValidationFails()
        {
            // Arrange
            var request = new RequestCreateAdminViewModel
            {
                Username = "", // Invalid to trigger validation
                Email = "invalidemail",
                Password = ""
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
                               .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<UserEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
            _domainNotificationMock.Verify(d => d.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);

            Assert.Null(result);
        }
    }
}
