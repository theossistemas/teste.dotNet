using Microsoft.EntityFrameworkCore;

namespace teste.dotNet.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) 
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
    }
}
