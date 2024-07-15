using LivrariaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaWeb.Data
{
    public class LivrariaDbContext : DbContext
    {
        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options) : base(options)
        {
        }

        public DbSet<LivroModel> Livros { get; set; }
    }
}
