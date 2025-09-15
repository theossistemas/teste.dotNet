using Livraria.Application.Services;
using Livraria.Domain.Interfaces;
using Livraria.Exceptions;
using Moq;
using Xunit;

public class LivroServiceTests
{
    private readonly Mock<ILivroRepository> _repositoryMock;
    private readonly LivroService _service;

    public LivroServiceTests()
    {
        _repositoryMock = new Mock<ILivroRepository>();
        _service = new LivroService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CriarLivro_DeveCriarComSucesso()
    {
        var livro = new Livro("Dom Casmurro", "Machado de Assis", 1899, 30m);
        _repositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(new List<Livro>());
        _repositoryMock.Setup(r => r.AdicionarAsync(livro)).ReturnsAsync(livro);

        var result = await _service.CriarLivroAsync(livro);

        Assert.Equal("Dom Casmurro", result.Titulo);
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Livro>()), Times.Once);
    }

    [Theory]
    [InlineData("O Alienista", "Machado de Assis", 1882, 25)]
    [InlineData("Dom Casmurro", "Machado de Assis", 1899, 30)]
    public async Task CriarLivro_DeveLancarExcecao_SeTituloEAutorJaExistirem(
     string titulo, string autor, int ano, decimal preco)
    {
        var livroExistente = new Livro(titulo, autor, ano, preco);
        var livroNovo = new Livro(titulo, autor, ano, preco);

        _repositoryMock.Setup(r => r.ListarAsync())
            .ReturnsAsync(new List<Livro> { livroExistente });

        await Assert.ThrowsAsync<LivroJaCadastradoExcepton>(
            () => _service.CriarLivroAsync(livroNovo));
    }

    [Fact]
    public async Task AtualizarLivro_DeveChamarRepositorio()
    {
        var livro = new Livro("Memórias Póstumas", "Machado de Assis", 1881, 28m);
        _repositoryMock.Setup(r => r.AtualizarAsync(livro)).ReturnsAsync(livro);

        var result = await _service.AtualizarLivroAsync(livro);

        Assert.Equal("Memórias Póstumas", result.Titulo);
        _repositoryMock.Verify(r => r.AtualizarAsync(livro), Times.Once);
    }

    [Fact]
    public async Task DeletarLivro_DeveChamarRepositorio()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.RemoverAsync(id)).ReturnsAsync(true);

        var result = await _service.DeletarLivroAsync(id);

        Assert.True(result);
        _repositoryMock.Verify(r => r.RemoverAsync(id), Times.Once);
    }

    [Fact]
    public async Task ObterLivroPorId_DeveRetornarLivro()
    {
        var livro = new Livro("O Senhor do Anéis - O Retorno do Rei", "J.R.R. Token", 1960, 320m);
        _repositoryMock.Setup(r => r.ObterPorIdAsync(livro.Id)).ReturnsAsync(livro);

        var result = await _service.ObterLivroPorIdAsync(livro.Id);

        Assert.Equal("O Senhor do Anéis - O Retorno do Rei", result.Titulo);
    }
}
