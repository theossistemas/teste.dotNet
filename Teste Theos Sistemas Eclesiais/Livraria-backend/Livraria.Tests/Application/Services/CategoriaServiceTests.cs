using Bogus;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Exceptions;
using Moq;
using Xunit;

public class CategoriaServiceTests
{
    private readonly Mock<ICategoriaRepository> _repositoryMock;
    private readonly CategoriaService _service;
    private readonly Faker _faker;
    public CategoriaServiceTests()
    {
        _repositoryMock = new Mock<ICategoriaRepository>();
        _service = new CategoriaService(_repositoryMock.Object);
        _faker = new Faker("pt_BR"); // localidade brasileira
    }

    [Fact]
    public async Task CriarCategoria_DeveCriarComSucesso()
    {
        // Arrange - gera um nome de categoria aleatório usando Faker
        var categoriaNome = _faker.Commerce.Categories(1)[0];
        var categoria = new Categoria(categoriaNome);

        _repositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(new List<Categoria>());
        _repositoryMock.Setup(r => r.AdicionarAsync(categoria)).ReturnsAsync(categoria);

        // Act
        var result = await _service.CriarCategoriaAsync(categoria);

        // Assert
        Assert.Equal(categoriaNome, result.Nome);
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Categoria>()), Times.Once);
    }


    [Fact]
    public async Task CriarCategoria_DeveLancarExcecao_SeJaExistir()
    {
        // Arrange
        var categoria = new Categoria("Romance");
        _repositoryMock.Setup(r => r.ListarAsync())
            .ReturnsAsync(new List<Categoria> { new Categoria("Romance") });

        // Act & Assert
        await Assert.ThrowsAsync<CategoriaJaExisteException>(() => _service.CriarCategoriaAsync(categoria));
    }

    [Fact]
    public async Task AtualizarCategoria_DeveAtualizarComSucesso()
    {
        var categoria = new Categoria("Terror");
        _repositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id)).ReturnsAsync(categoria);
        _repositoryMock.Setup(r => r.AtualizarAsync(categoria)).ReturnsAsync(categoria);

        var result = await _service.AtualizarCategoriaAsync(categoria);

        Assert.Equal("Terror", result.Nome);
        _repositoryMock.Verify(r => r.AtualizarAsync(categoria), Times.Once);
    }

    [Fact]
    public async Task AtualizarCategoria_DeveLancarExcecao_SeNaoEncontrada()
    {
        // Arrange - gera nome de categoria fake
        var categoriaNome = _faker.Commerce.Categories(1)[0];
        var categoria = new Categoria(categoriaNome);

        // Simula que o repositório não achou a categoria
        _repositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id)).ReturnsAsync((Categoria)null);

        // Act & Assert
        await Assert.ThrowsAsync<CategoriaNaoEncontradaException>(() => _service.AtualizarCategoriaAsync(categoria));
    }

    [Fact]
    public async Task DeletarCategoria_DeveDeletarComSucesso()
    {
        var categoria = new Categoria("História");
        _repositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id)).ReturnsAsync(categoria);
        _repositoryMock.Setup(r => r.RemoverAsync(categoria.Id)).ReturnsAsync(true);

        var result = await _service.DeletarCategoriaAsync(categoria.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeletarCategoria_DeveLancarExcecao_SeNaoEncontrada()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync((Categoria)null);

        await Assert.ThrowsAsync<CategoriaNaoEncontradaException>(() => _service.DeletarCategoriaAsync(id));
    }
}
