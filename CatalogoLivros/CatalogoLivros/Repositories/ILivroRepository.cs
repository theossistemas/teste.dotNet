using CatalogoLivros.Models;

namespace CatalogoLivros.Repositories;

public interface ILivroRepository
{
    IEnumerable<Livro> GetLivros();
    Livro GetLivroPorId(int id);
    Livro Create(Livro livro);
    Livro Update(Livro livro);
    Livro Delete(int id);
    bool JaExisteCadastroLivro(string nome, string autor, int ano);
}
