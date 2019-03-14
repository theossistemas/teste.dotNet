using livraria.Domain.entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace livraria.Context
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> options) : base(options)
        {

        }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Livro> Livro { get; set; }

    }
}
