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
    public class CriarLivroCommandHandlerTests
    {
        [Fact]
        public async Task CriarLivro_Com_Sucesso()
        {
            var command = new CriarLivroCommand(
                "Senhor dos Aneis",
                "JK Tolkien",
                "1234567890123",
                new DateTime(2020, 1, 1),
                50.99m,
                10
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ExisteLivroComNome(command.Titulo)).ReturnsAsync(false);
            livroRepositoryMock.Setup(r => r.Cadastrar(It.IsAny<Livro>())).ReturnsAsync((Livro livro) => livro);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(1);

            var handler = new CriarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.NotEqual(1, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public async Task Falha_SeLivroJaExiste()
        {
            var command = new CriarLivroCommand(
                "Senhor dos Aneis",
                "JK Tolkien",
                "1234567890123",
                new DateTime(2020, 1, 1),
                50.99m,
                10
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ExisteLivroComNome(command.Titulo)).ReturnsAsync(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);

            var handler = new CriarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
            Assert.Equal("Já existe um Livro cadastrado com este título.", result.Message);
        }

        [Fact]
        public async Task Falha_SeExcecaoForLancada()
        {
            var command = new CriarLivroCommand(
                "Senhor dos Aneis Pior Livro",
                "Autor Teste",
                "1234567890123",
                new DateTime(2020, 1, 1),
                50.99m,
                10
            );

            var livroRepositoryMock = new Mock<ILivroRepository>();
            livroRepositoryMock.Setup(r => r.ExisteLivroComNome(command.Titulo)).ThrowsAsync(new Exception("Erro inesperado e não analisado nos requisitos."));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.LivroRepository).Returns(livroRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.Rollback()).Returns(Task.CompletedTask);

            var handler = new CriarLivroCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Erro inesperado e não analisado nos requisitos.", result.Exception?.Message);
        }
    }
}