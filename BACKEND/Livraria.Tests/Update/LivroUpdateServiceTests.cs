using Livraria.Service;
using Livraria.Infrastructure.Interfaces;
using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Livraria.Tests.Update
{
    public class LivroUpdateServiceTests
    {
        private readonly Mock<ILivroUpdateRepository> _updateRepoMock;
        private readonly Mock<ILivroReadRepository> _readRepoMock;
        private readonly Mock<ILogger<LivroUpdateService>> _loggerMock;
        private readonly LivroUpdateService _service;

        public LivroUpdateServiceTests()
        {
            _updateRepoMock = new Mock<ILivroUpdateRepository>();
            _readRepoMock = new Mock<ILivroReadRepository>();
            _loggerMock = new Mock<ILogger<LivroUpdateService>>();
            _service = new LivroUpdateService(_updateRepoMock.Object, _readRepoMock.Object, _loggerMock.Object);
        }

        private LivroDTO GetLivroDto() =>
            new LivroDTO
            {
                Titulo = "Novo Título",
                Autor = "Novo Autor",
                ISBN = "9876543210",
                AnoPublicacao = 2021,
                Quantidade = 5,
                Preco = 49.99m
            };

        private Livro GetLivro(int id, bool ativo = true) =>
            new Livro(id, "Antigo Título", "Antigo Autor", "1234567890", 2020, 10, 99.99m)
            {
                Ativo = ativo
            };

        [Fact]
        public async Task UpdateLivroAsync_DeveRetornarLivroDTO_QuandoAtualizacaoBemSucedida()
        {
            var livro = GetLivro(1);
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);
            _updateRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Livro>())).ReturnsAsync(livro);

            var result = await _service.UpdateLivroAsync(1, GetLivroDto());

            Assert.NotNull(result);
            Assert.Equal("Novo Título", result.Titulo);
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveRetornarNull_QuandoLivroNaoEncontrado()
        {
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Livro)null);

            var result = await _service.UpdateLivroAsync(1, GetLivroDto());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveRetornarNull_QuandoLivroInativo()
        {
            var livro = GetLivro(1, ativo: false);
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);

            var result = await _service.UpdateLivroAsync(1, GetLivroDto());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveRetornarNull_QuandoNaoEncontradoException()
        {
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ThrowsAsync(new NaoEncontradoException());

            var result = await _service.UpdateLivroAsync(1, GetLivroDto());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveRetornarNull_QuandoOperacaoNaoPermitidaException()
        {
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(GetLivro(1));
            _updateRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Livro>())).ThrowsAsync(new OperacaoNaoPermitidaException());

            var result = await _service.UpdateLivroAsync(1, GetLivroDto());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveLancarConflitoDeDadosException()
        {
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(GetLivro(1));
            _updateRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Livro>())).ThrowsAsync(new ConflitoDeDadosException());

            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => _service.UpdateLivroAsync(1, GetLivroDto()));
        }

        [Fact]
        public async Task UpdateLivroAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
        {
            _readRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(GetLivro(1));
            _updateRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Livro>())).ThrowsAsync(new Exception("Erro genérico"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.UpdateLivroAsync(1, GetLivroDto()));
            Assert.Contains("Erro inesperado ao atualizar livro", ex.Message);
        }
    }
}
