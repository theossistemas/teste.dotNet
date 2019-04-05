using ProjetoLivraria.Data.EntityConfiguration;
using ProjetoLivraria.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjetoLivraria.Data.Context
{
    public class LivrariaContext : DbContext
    {

        public LivrariaContext()
            : base("ProjetoLivraria")
        {
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new LivrariaConfiguration());
          
        }
    }
}
