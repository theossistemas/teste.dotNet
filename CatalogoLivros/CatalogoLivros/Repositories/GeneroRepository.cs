using CatalogoLivros.Context;
using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;


namespace CatalogoLivros.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly CatalogoLivrosContext _context;

    public GeneroRepository(CatalogoLivrosContext context)
    {
        _context = context;
    }
    public IEnumerable<Genero> GetGenerosLivros()
    {
        return _context.Generos.Include(g => g.Livros).ToList();
    }
    public IEnumerable<Genero> GetGeneros()
    {
        return _context.Generos.ToList();
    }
    public Genero GetGeneroPorId(int id)
    {
        return _context.Generos.FirstOrDefault(g => g.Id == id);
    }
    public Genero Create(Genero genero)
    {
        if (genero is null)
            throw new ArgumentNullException(nameof(genero));

        _context.Generos.Add(genero);
        _context.SaveChanges();

        return genero;
    }
    public Genero Update(Genero genero)
    {
        if (genero is null)
            throw new ArgumentNullException(nameof(genero));

        _context.Entry(genero).State = EntityState.Modified;
        _context.SaveChanges();

        return genero;
    }
    public Genero Delete(int id)
    {
        var genero = _context.Generos.Find(id);

        if (genero is null)
            throw new ArgumentNullException(nameof(genero));

        _context.Generos.Remove(genero);
        _context.SaveChanges();

        return genero;
    }
    public bool JaExisteCadastroGenero(string nome)
    {
        var genero = _context.Generos.Where(g => g.Nome.ToUpper() == nome.ToUpper()).ToList();
        if (genero == null || genero.Count() == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
