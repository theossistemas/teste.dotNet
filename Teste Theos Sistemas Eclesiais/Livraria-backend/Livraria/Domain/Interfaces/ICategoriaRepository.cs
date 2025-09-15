using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria> AdicionarAsync(Categoria categoria);
        Task<Categoria> AtualizarAsync(Categoria categoria);
        Task<bool> RemoverAsync(Guid id);
        Task<Categoria> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Categoria>> ListarAsync();
    }

}
