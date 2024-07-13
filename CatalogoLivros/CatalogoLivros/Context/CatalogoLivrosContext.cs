using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Context;

public class CatalogoLivrosContext : DbContext
{
    public CatalogoLivrosContext(DbContextOptions<CatalogoLivrosContext> options) 
        : base(options)
    {
    }

    public DbSet<Genero> Generos { get; set; }
    public DbSet<Livro> Livros { get; set; }
}
