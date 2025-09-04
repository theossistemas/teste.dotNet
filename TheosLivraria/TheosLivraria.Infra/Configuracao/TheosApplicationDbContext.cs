using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Infra.Configuracao
{
    public class TheosApplicationDbContext(DbContextOptions<TheosApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }
}
