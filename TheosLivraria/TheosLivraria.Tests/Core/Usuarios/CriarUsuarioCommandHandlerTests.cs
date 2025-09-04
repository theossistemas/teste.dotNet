using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Tests.Core.Usuarios
{
    public class CriarUsuarioCommandHandlerTests
    {
        [Fact]
        public async Task CriarUsuario_Com_Sucesso()
        {
            
            var command = new CriarUsuarioCommand(
                "Carlos",
                "12345678900",
                "carlos@email.com",
                "senha123",
                "11999999999",
                new DateTime(1990, 1, 1),
                (int)Perfil.Administrador
            );

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorEmail(command.Email)).ReturnsAsync((Usuario)null);
            usuarioRepositoryMock.Setup(r => r.Cadastrar(It.IsAny<Usuario>())).ReturnsAsync((Usuario usuario) => usuario);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(1);

            var handler = new CriarUsuarioCommandHandler(unitOfWorkMock.Object);

            // Act
            var result = await handler.ExecuteAsync(command);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.NotEqual(1, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public async Task RetornarFalha_SeUsuarioJaExiste()
        {
            // Arrange
            var command = new CriarUsuarioCommand(
                "Carlos",
                "12345678900",
                "carlos@email.com",
                "senha123",
                "11999999999",
                new DateTime(1990, 1, 1),
                (int)Perfil.Administrador
            );

            var usuarioExistente = new Usuario(command.Nome, command.Documento, command.Email, command.Senha, command.Telefone, command.DataAniversario, Perfil.Administrador);

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorEmail(command.Email)).ReturnsAsync(usuarioExistente);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);

            var handler = new CriarUsuarioCommandHandler(unitOfWorkMock.Object);

            
            var result = await handler.ExecuteAsync(command);

            
            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
            Assert.Equal("Já existe um usuário cadastrado com este e-mail.", result.Message);
        }

        [Fact]
        public async Task RetornarFalha_SeExcecaoForLancada()
        {
            var command = new CriarUsuarioCommand(
                "Carlos",
                "12345678900",
                "carlos@email.com",
                "senha123",
                "11999999999",
                new DateTime(1990, 1, 1),
                (int)Perfil.Administrador
            );

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock.Setup(r => r.ObterPorEmail(command.Email)).ThrowsAsync(new Exception("Emeail não localizado"));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(u => u.UsuarioRepository).Returns(usuarioRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.Rollback()).Returns(Task.CompletedTask);

            var handler = new CriarUsuarioCommandHandler(unitOfWorkMock.Object);

            
            var result = await handler.ExecuteAsync(command);

            
            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Emeail não localizado", result.Message);
            Assert.NotNull(result.Exception);
        }
    }
}
