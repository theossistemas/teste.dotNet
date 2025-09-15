using Livraria.API.DTOs.Livros;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro> AdicionarAsync(Livro livro);
        Task<Livro> AtualizarAsync(Livro livro);
        Task<bool> RemoverAsync(Guid id);
        Task<Livro> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Livro>> ListarAsync();
    }

}
