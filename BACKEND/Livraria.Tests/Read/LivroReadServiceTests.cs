using Livraria.Service;
using Livraria.Infrastructure.Interfaces;
using Livraria.Domain.Exceptions;
using Livraria.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Livraria.Tests.Read
{
    public class LivroReadServiceTests
    {
        private readonly Mock<ILivroReadRepository> _repoMock;
        private readonly Mock<ILogger<LivroReadService>> _loggerMock;
        private readonly LivroReadService _service;

        public LivroReadServiceTests()
        {
            _repoMock = new Mock<ILivroReadRepository>();
            _loggerMock = new Mock<ILogger<LivroReadService>>();
            _service = new LivroReadService(_repoMock.Object, _loggerMock.Object);
        }

        private Livro GetLivro(int id, bool ativo = true) =>
            new Livro(id, "Título", "Autor", "1234567890", 2020, 10, 99.99m)
            {
                Ativo = ativo
            };

        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarLivroDTO_QuandoLivroAtivoEncontrado()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(GetLivro(1, true));

            var result = await _service.GetLivroByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Título", result.Titulo);
        }

        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarNull_QuandoLivroNaoEncontrado()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Livro)null);

            var result = await _service.GetLivroByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarNull_QuandoLivroInativo()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(GetLivro(1, false));

            var result = await _service.GetLivroByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarNull_QuandoNaoEncontradoException()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ThrowsAsync(new NaoEncontradoException());

            var result = await _service.GetLivroByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLivroByIdAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ThrowsAsync(new Exception("Erro genérico"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.GetLivroByIdAsync(1));
            Assert.Contains("Erro inesperado ao buscar livro", ex.Message);
        }

        [Fact]
        public async Task GetLivrosAsync_DeveRetornarLivrosDTO_QuandoLivrosAtivosExistem()
        {
            var livros = new List<Livro>
            {
                GetLivro(1, true),
                GetLivro(2, false),
                GetLivro(3, true)
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(livros);

            var result = await _service.GetLivrosAsync();

            Assert.NotNull(result);
            Assert.All(result, l => Assert.True(l.Ativo));
        }

        [Fact]
        public async Task GetLivrosAsync_DeveRetornarVazio_QuandoNaoEncontradoException()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new NaoEncontradoException());

            var result = await _service.GetLivrosAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLivrosAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("Erro genérico"));

            await Assert.ThrowsAsync<ApplicationException>(() => _service.GetLivrosAsync());
        }

        [Fact]
        public async Task GetLivrosPagedAsync_DeveRetornarLivrosDTOeTotalCount_QuandoSucesso()
        {
            var livros = new List<Livro> { GetLivro(1, true), GetLivro(2, true) };
            _repoMock.Setup(r => r.GetPagedAsync(1, 2)).ReturnsAsync((livros, livros.Count));

            var (result, totalCount) = await _service.GetLivrosPagedAsync(1, 2);

            Assert.NotNull(result);
            Assert.Equal(2, totalCount);
        }

        [Fact]
        public async Task GetLivrosPagedAsync_DeveRetornarVazioEZero_QuandoNaoEncontradoException()
        {
            _repoMock.Setup(r => r.GetPagedAsync(1, 2)).ThrowsAsync(new NaoEncontradoException());

            var (result, totalCount) = await _service.GetLivrosPagedAsync(1, 2);

            Assert.Empty(result);
            Assert.Equal(0, totalCount);
        }

        [Fact]
        public async Task GetLivrosPagedAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
        {
            _repoMock.Setup(r => r.GetPagedAsync(1, 2)).ThrowsAsync(new Exception("Erro genérico"));

            await Assert.ThrowsAsync<ApplicationException>(() => _service.GetLivrosPagedAsync(1, 2));
        }
    }
}
