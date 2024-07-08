using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.Core.Entities.Logs;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Livraria.Infrastructure.Data.Mappings
{
    public class LivrariaDbContext : DbContext
    {
        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options) : base(options)
        {

        }
        public DbSet<LivroEntity> Livros { get; set; }
        public DbSet<AutorEntity> Autores { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<LogsEntity> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorEntity>()
                .HasMany(x => x.Livros)
                .WithOne(x => x.Autor)
                .HasForeignKey(x => x.AutorId);

            modelBuilder.Entity<CategoriaEntity>()
                .HasMany(x => x.Livros)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}