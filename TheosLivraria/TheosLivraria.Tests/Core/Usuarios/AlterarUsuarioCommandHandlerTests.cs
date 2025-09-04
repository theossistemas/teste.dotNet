using Moq;
using System.Net;
using System.Threading.Tasks;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using Xunit;

namespace TheosLivraria.Tests.Core.Usuarios
{
    public class AlterarUsuarioCommandHandlerTests
    {
        [Fact]
        public async Task AlterarUsuario_Com_Sucesso()
        {
            var command = new AlterarUsuarioCommand(
                Guid.NewGuid(),
                "Novo Nome",
                "12345678900",
                "novo@email.com",
                "11999999999",
                new DateTime(1991, 2, 2)
            );


            var usuarioMock = new Usuario(
                            "Nome Antigo",
                            "12345678900",
                            "antigo@email.com",
                            "senha123",
                            "11999999999",
                            new DateTime(1990, 1, 1),
                            Perfil.Administrador
                        );

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ReturnsAsync(usuarioMock);


            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(1);

            var handler = new AlterarUsuarioCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.Data);
            Assert.Equal("Usuário alterado com sucesso.", result.Message);
        }

        [Fact]
        public async Task Falha_SeUsuarioNaoEncontrado()
        {
            var command = new AlterarUsuarioCommand(
                Guid.NewGuid(),
                "Nome",
                "12345678900",
                "email@email.com",
                "11999999999",
                new DateTime(1990, 1, 1)
            );

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ReturnsAsync((Usuario)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);

            var handler = new AlterarUsuarioCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Usuário não encontrado.", result.Message);
        }

        [Fact]
        public async Task Falha_SeExcecaoForLancada()
        {
            var command = new AlterarUsuarioCommand(
                Guid.NewGuid(),
                "Nome",
                "12345678900",
                "email@email.com",
                "11999999999",
                new DateTime(1990, 1, 1)
            );

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorUuId(command.Id)).ThrowsAsync(new Exception("Erro inesperado"));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.Rollback()).Returns(Task.CompletedTask);

            var handler = new AlterarUsuarioCommandHandler(unitOfWorkMock.Object);

            var result = await handler.ExecuteAsync(command);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Erro ao alterar usuário.", result.Message);
            Assert.NotNull(result.Exception);
        }
    }
}