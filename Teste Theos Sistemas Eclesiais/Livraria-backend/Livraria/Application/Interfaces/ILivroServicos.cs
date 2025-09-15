using Livraria.API.DTOs.Livros;

namespace Livraria.Aplicacao.Interfaces
{
    public interface ILivroService
    {
        Task<Livro> CriarLivroAsync(Livro livro);
        Task<Livro> AtualizarLivroAsync(Livro livro);
        Task<bool> DeletarLivroAsync(Guid id);
        Task<Livro> ObterLivroPorIdAsync(Guid id);
        Task<IEnumerable<Livro>> ListarLivrosAsync();
    }
}
