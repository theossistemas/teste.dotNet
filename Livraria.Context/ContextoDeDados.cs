using Livraria.Context.Types;
using Livraria.Context.Types.ManyToMany;
using Livraria.Domain.Contexto;
using Livraria.Domain.Livros;
using Livraria.Domain.ManyToMany;
using Livraria.Domain.Pessoas;
using Livraria.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace Livraria.Context
{
    public class ContextoDeDados : DbContext, IContextoDeDados
    {
        public ContextoDeDados(DbContextOptions<ContextoDeDados> options) : base(options)
        {

        }

        public DbSet<AutorLivro> AutoresLivros { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroTema> LivrosTemas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AutorLivroTypeConfiguration());
            builder.ApplyConfiguration(new LivroTemaTypeConfiguration());

            builder.ApplyConfiguration(new PessoaTypeConfiguration());
            builder.ApplyConfiguration(new LivroTypeConfiguration());
            builder.ApplyConfiguration(new TemaTypeConfiguration());
            builder.ApplyConfiguration(new UsuarioTypeConfiguration());
        }
    }
}
