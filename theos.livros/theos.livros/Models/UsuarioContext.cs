using Microsoft.EntityFrameworkCore;
using theos.livros.Entitys;

namespace theos.livros.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext()
        {
        }

        public UsuarioContext(DbContextOptions<UsuarioContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=JOSE-PC\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            }
        }
    }
}
