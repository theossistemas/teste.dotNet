using Livraria.Domain.Livros;
using Livraria.Domain.ManyToMany;
using Livraria.Domain.Pessoas;
using Livraria.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace Livraria.Domain.Contexto
{
    public interface IContextoDeDados : IDbContext
    {
        DbSet<AutorLivro> AutoresLivros { get; }
        DbSet<Livro> Livros { get; }
        DbSet<LivroTema> LivrosTemas { get; }
        DbSet<Pessoa> Pessoas { get; }
        DbSet<Tema> Temas { get; }
        DbSet<Usuario> Usuarios { get; }
    }
}
