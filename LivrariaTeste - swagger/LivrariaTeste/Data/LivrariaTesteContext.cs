using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTeste.Data
{
   public class LivrariaTesteContext : DbContext
   {
      public LivrariaTesteContext(DbContextOptions<LivrariaTesteContext> options)
          : base(options)
      {
      }

      public DbSet<LivrariaTeste.Models.Livros> Livros { get; set; }
   }
}
