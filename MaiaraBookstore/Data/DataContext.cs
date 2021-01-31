using MaiaraBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace MaiaraBookstore.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options): base(options) {}

        public DbSet<Livro> Livro { get; set; }
        public DbSet<LogBookstore> LogBookstores { get; set; }

    }
}
