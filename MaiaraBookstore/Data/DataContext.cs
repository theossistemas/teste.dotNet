using MaiaraBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace MaiaraBookstore.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options): base(options) {}
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogBookstore>()
                .HasOne<Livro>(l => l.Livro)
                .WithMany(l => l.Logsbookstore)
                .HasForeignKey(l => l.LivroId);
        }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<LogBookstore> LogBookstores { get; set; }

    }
}
