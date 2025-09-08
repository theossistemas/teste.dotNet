using Livraria.Service;
using Livraria.Infrastructure.Interfaces;
using Livraria.Domain.Exceptions;
using Livraria.DTOs;
using Livraria.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Livraria.Tests.Create
{
    public class LivroCreateServiceTests
    {
        private readonly Mock<ILivroCreateRepository> _repoMock;
        private readonly Mock<ILogger<LivroCreateService>> _loggerMock;
        private readonly LivroCreateService _service;

        public LivroCreateServiceTests()
        {
            _repoMock = new Mock<ILivroCreateRepository>();
            _loggerMock = new Mock<ILogger<LivroCreateService>>();
            _service = new LivroCreateService(_repoMock.Object, _loggerMock.Object);
        }

        private LivroDTO GetLivroDto() =>
            new LivroDTO
            {
                Titulo = "Teste",
                Autor = "Autor",
                ISBN = "1234567890",
                AnoPublicacao = 2020,
                Quantidade = 10,
                Preco = 99.99m
            };

        [Fact]
        public async Task AddLivroAsync_DeveRetornarLivroDTO_QuandoCriacaoBemSucedida()
        {
            var livro = new Livro(0, "Teste", "Autor", "1234567890", 2020, 10, 99.99m);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Livro>())).ReturnsAsync(livro);

            var result = await _service.AddLivroAsync(GetLivroDto());

            Assert.NotNull(result);
            Assert.Equal("Teste", result.Titulo);
        }

        [Fact]
        public async Task AddLivroAsync_DeveRetornarNull_QuandoOperacaoNaoPermitidaException()
        {
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Livro>())).ThrowsAsync(new OperacaoNaoPermitidaException());

            var result = await _service.AddLivroAsync(GetLivroDto());

            Assert.Null(result);
        }

        [Fact]
        public async Task AddLivroAsync_DeveLancarConflitoDeDadosException()
        {
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Livro>())).ThrowsAsync(new ConflitoDeDadosException());

            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => _service.AddLivroAsync(GetLivroDto()));
        }

        [Fact]
        public async Task AddLivroAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
        {
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Livro>())).ThrowsAsync(new Exception("Erro genérico"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.AddLivroAsync(GetLivroDto()));
            Assert.Contains("Erro inesperado ao criar livro", ex.Message);
        }
    }
}
