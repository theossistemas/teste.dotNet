using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Exceptions;
using Serilog;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
        Log.Information("CategoriaService inicializada");
    }

    public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
    {
        Log.Information("Criando a categoria", categoria.Nome);
        var existente = await _repository.ListarAsync();
        if (existente.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
            throw new CategoriaJaExisteException(categoria.Nome);

        return await _repository.AdicionarAsync(categoria);
    }

    public async Task<Categoria> AtualizarCategoriaAsync(Categoria categoria)
    {
        Log.Information("Criando a categoria", categoria.Nome);

        var existente = await _repository.ObterPorIdAsync(categoria.Id);
        if (existente == null)
        {
            Log.Warning("Categoria {Nome} já cadastrado", categoria.Nome);
            throw new CategoriaNaoEncontradaException(categoria.Id);
        }

        var result = await _repository.AtualizarAsync(categoria);
        Log.Information("Categoria {Nome} criado com sucesso", categoria.Nome);
        return result;

    }

    public async Task<bool> DeletarCategoriaAsync(Guid id)
    {
        var existente = await _repository.ObterPorIdAsync(id);
        if (existente == null)
            throw new CategoriaNaoEncontradaException(id);

        return await _repository.RemoverAsync(id);
    }

    public async Task<Categoria> ObterCategoriaPorIdAsync(Guid id)
    {
        var categoria = await _repository.ObterPorIdAsync(id);
        if (categoria == null)
            throw new CategoriaNaoEncontradaException(id);

        return categoria;
    }

    public async Task<IEnumerable<Categoria>> ListarCategoriasAsync()
        => (await _repository.ListarAsync()).OrderBy(c => c.Nome);
}
