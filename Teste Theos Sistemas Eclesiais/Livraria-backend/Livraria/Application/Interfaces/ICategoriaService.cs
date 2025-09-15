using Livraria.Domain.Entities;

namespace Livraria.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> CriarCategoriaAsync(Categoria categoria);
        Task<Categoria> AtualizarCategoriaAsync(Categoria categoria);
        Task<bool> DeletarCategoriaAsync(Guid id);
        Task<Categoria> ObterCategoriaPorIdAsync(Guid id);
        Task<IEnumerable<Categoria>> ListarCategoriasAsync();
    }

}
