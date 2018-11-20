using Biblioteca.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> option  ) : base (option)
        {

            Database.EnsureCreated();

        }

        public DbSet<Livro> Livro { get; set; }
    }
}
