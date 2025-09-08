
using Livraria.Infrastructure.Interfaces;
using Livraria.Service.Interfaces;
using Livraria.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Livraria.Service;

public class LivroDeleteServiceTests
{
    private readonly Mock<ILivroDeleteRepository> _repoMock;
    private readonly Mock<ILogger<LivroDeleteService>> _loggerMock;
    private readonly LivroDeleteService _service;

    public LivroDeleteServiceTests()
    {
        _repoMock = new Mock<ILivroDeleteRepository>();
        _loggerMock = new Mock<ILogger<LivroDeleteService>>();
        _service = new LivroDeleteService(_repoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task DeleteLivroAsync_DeveRetornarTrue_QuandoExclusaoBemSucedida()
    {
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await _service.DeleteLivroAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteLivroAsync_DeveRetornarFalse_QuandoNaoEncontradoException()
    {
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new NaoEncontradoException());

        var result = await _service.DeleteLivroAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteLivroAsync_DeveRetornarFalse_QuandoOperacaoNaoPermitidaException()
    {
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new OperacaoNaoPermitidaException());

        var result = await _service.DeleteLivroAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteLivroAsync_DeveLancarConflitoDeDadosException()
    {
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new ConflitoDeDadosException());

        await Assert.ThrowsAsync<ConflitoDeDadosException>(() => _service.DeleteLivroAsync(1));
    }

    [Fact]
    public async Task DeleteLivroAsync_DeveLancarApplicationException_QuandoExceptionGenerica()
    {
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Erro genérico"));

        var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.DeleteLivroAsync(1));
        Assert.Contains("Erro inesperado ao excluir livro", ex.Message);
    }
}