using Microsoft.EntityFrameworkCore;
using theos.livros.Entitys;

namespace theos.livros.Models
{
    public class LivroContext : DbContext
    {
        public LivroContext()
        {
        }

        public LivroContext(DbContextOptions<LivroContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=JOSE-PC\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            }
        }
    }
}
