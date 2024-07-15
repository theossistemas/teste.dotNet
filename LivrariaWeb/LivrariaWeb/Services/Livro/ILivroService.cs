using LivrariaWeb.Models;

namespace LivrariaWeb.Services.Livro
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroModel>> ListarLivros();
        Task<LivroModel> BuscarLivroPorId(int? idLivro);
        Task<LivroModel> CriarLivro(LivroModel livroDTO);
        Task<LivroModel> EditarLivro(LivroModel livroDTO);
        Task<bool> ExcluirLivro(int? idLivro);
    }
}
