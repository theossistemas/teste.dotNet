using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Application.UseCases.User.Create;
using TheosLivraria.Application.UseCases.User.Create.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using Xunit;

namespace TheosLivraria.Tests.UseCases.User
{
    public class CreateUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<CreateUserInput>> _validatorMock;
        private readonly CreateUserUseCase _useCase;

        public CreateUserUseCaseTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<CreateUserInput>>();

            _useCase = new CreateUserUseCase(
                _userRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _validatorMock.Object
            );
        }

        private static CreateUserInput GetValidInput() =>
            new CreateUserInput
            {
                Name = "Teste da Silva",
                Email = "teste@test.com",
                Password = "123456"
            };

        [Fact(DisplayName = "Deve falhar quando a validação retornar erros")]
        public async Task ExecuteAsync_ShouldFail_WhenValidationFails()
        {
            var input = GetValidInput();
            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("Email", "Email inválido")
            });

            _validatorMock
                .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("Erro de validação na entidade.", result.ErrorMessage);
            Assert.NotNull(result.MetaData);

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<UserEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Deve falhar quando o e-mail já estiver em uso")]
        public async Task ExecuteAsync_ShouldFail_WhenEmailAlreadyExists()
        {
            var input = GetValidInput();

            _validatorMock
                .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(input.Email))
                .ReturnsAsync(new UserEntity { Id = Guid.NewGuid(), Email = input.Email });

            var result = await _useCase.ExecuteAsync(input);

            Assert.False(result.Success);
            Assert.Equal("O e-mail informado já está em uso.", result.ErrorMessage);

            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<UserEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Deve criar usuário com sucesso quando os dados forem válidos")]
        public async Task ExecuteAsync_ShouldSucceed_WhenUserIsCreated()
        {
            var input = GetValidInput();
            var generatedId = Guid.NewGuid();

            _validatorMock
                .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(input.Email))
                .ReturnsAsync((UserEntity)null);

            _userRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(u => u.Id = generatedId)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

            var result = await _useCase.ExecuteAsync(input);

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(generatedId, result.Value.Id);

            _userRepositoryMock.Verify(r => r.AddAsync(It.Is<UserEntity>(
                u => u.Email == input.Email && u.Name == input.Name
            )), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
