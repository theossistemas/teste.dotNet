using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class LivroDbContext : DbContext
    {
        public LivroDbContext(DbContextOptions<LivroDbContext> options) : base(options)
        {

        }

        public DbSet<Livro> Livros { get; set; }
    }
}
