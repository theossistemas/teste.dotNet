using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using TheosLivraria.Core.Command.Livros;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using Xunit;

namespace TheosLivraria.Tests.Core.Livros
{
    public class AlterarLivroCommandHandlerTests
    {
        [Fact]
        public async Task AlterarLivro_Com_Sucesso()
        {
            var command = new AlterarLivroCommand(
                Guid.NewGuid(),
                "Senhor dos Aneis - O Retorno do Rei",
                "J K Tolkien",
                "1234567890123",
                new DateTime(1932, 2, 7),
                99.99m,
                20
            );

            var livroMock = new Livro(
                "Senhor dos Aneis",
                "Tokien",
                "1234567890123",
                new DateTime(1933, 12, 4),
                199.99m,
                5
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ReturnsAsync(livroMock);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(1);

            var handler = new AlterarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.Data);
            Assert.Equal("Livro alterado com sucesso.", result.Message);
        }

        [Fact]
        public async Task SeLivroNaoEncontrado()
        {
            var command = new AlterarLivroCommand(
                Guid.NewGuid(),
               "Senhor dos Aneis - O Retorno do Rei",
                "J K Tolkien",
                "1234567890123",
                new DateTime(2020, 1, 1),
                10.99m,
                5
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ReturnsAsync((Livro)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);

            var handler = new AlterarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Livro não encontrado.", result.Message);
        }

        [Fact]
        public async Task SeExcecaoForLancada()
        {
            var command = new AlterarLivroCommand(
                Guid.NewGuid(),
               "Senhor dos Aneis - O Retorno do Rei é ruim",
                "J K Tolkien",
                "1234567890123",
                new DateTime(2020, 1, 1),
                10.99m,
                5
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ThrowsAsync(new Exception("Erro inesperado"));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.Rollback()).Returns(Task.CompletedTask);

            var handler = new AlterarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Erro ao alterar Livro.", result.Message);
            Assert.NotNull(result.Exception);
        }
    }
}