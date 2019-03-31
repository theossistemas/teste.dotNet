using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjetoLivraria.Data.Mappings;
using ProjetoLivraria.Domain.Entities;

namespace ProjetoLivraria.Data
{
    public class ProjetoLivrariaContext : DbContext
    {
        public ProjetoLivrariaContext(DbContextOptions<ProjetoLivrariaContext> options)
            : base(options) { }

        public DbSet<Livro> Livros { get; set; }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).CriadoEm = now;
                }
                else
                    ((Entity)entity.Entity).ModificadoEm = now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());

            base.OnModelCreating(modelBuilder);
        }

    }

    public class ProjetoLivrariaContextFactory : IDesignTimeDbContextFactory<ProjetoLivrariaContext>
    {

        public ProjetoLivrariaContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<ProjetoLivrariaContext>();
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=LivrariaDb;User ID=sa;password=Iso@9001;");

            return new ProjetoLivrariaContext(optionsBuilder.Options);
        }
    }
}
