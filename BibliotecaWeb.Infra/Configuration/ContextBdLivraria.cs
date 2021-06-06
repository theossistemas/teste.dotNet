using LivrariaWeb.Domain.Model;
using LivrariaWeb.Infra.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaWeb.Infra.Configuration
{
    public class ContextBdLivraria : DbContext
    {
        public ContextBdLivraria(DbContextOptions<ContextBdLivraria> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Livro> Livros { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
        }
    }
}
