using CatalogoLivros.Context;
using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly CatalogoLivrosContext _context;
    public LivroRepository(CatalogoLivrosContext context)
    {
        _context = context;
    }
    public IEnumerable<Livro> GetLivros()
    {
        return _context.Livros.ToList().OrderBy(o => o.Nome);
    }
    public Livro GetLivroPorId(int id)
    {
        return _context.Livros.FirstOrDefault(l => l.Id == id);
    }
    public Livro Create(Livro livro)
    {
        if (livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Livros.Add(livro);
        _context.SaveChanges();

        return livro;
    }
    public Livro Update(Livro livro)
    {
        if(livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Entry(livro).State = EntityState.Modified;
        _context.SaveChanges();

        return livro;
    }
    public Livro Delete(int id)
    {
        var livro = _context.Livros.Find(id);

        if (livro is null)
            throw new ArgumentNullException(nameof(livro));

        _context.Livros.Remove(livro);
        _context.SaveChanges();

        return livro;
    }
    public bool JaExisteCadastroLivro(string nome, string autor, int ano)
    {
        var livro = _context.Livros.Where(l => l.Nome.ToUpper() == nome.ToUpper()
                                             && l.Autor.ToUpper() == autor.ToUpper()
                                             && l.Ano == ano).ToList();

        if (livro == null || livro.Count() == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
