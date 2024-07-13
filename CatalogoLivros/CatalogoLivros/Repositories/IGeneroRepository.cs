using CatalogoLivros.Models;

namespace CatalogoLivros.Repositories;

public interface IGeneroRepository
{
    IEnumerable<Genero> GetGenerosLivros();
    IEnumerable<Genero> GetGeneros();
    Genero GetGeneroPorId(int id);
    Genero Create(Genero genero);
    Genero Update(Genero genero);
    Genero Delete(int id);
    bool JaExisteCadastroGenero(string nome);
}
