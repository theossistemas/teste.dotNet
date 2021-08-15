using LivrariaTheos.Core.Data;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Generos;
using LivrariaTheos.Estoque.Domain.Livros;
using LivrariaTheos.Estoque.Domain.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Data
{
    public class EstoqueContext : DbContext, IUnitOfWork
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
        {

        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LogAplicacao> LogsAplicacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstoqueContext).Assembly);
        }

        private static void ConfiguracaoEntidadeBase(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "UsuarioInclusao")))
            {
                property.SetColumnType($"varchar(50)");
                property.SetColumnName("InclusaoUsuario");
                property.IsNullable = false;
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "DataInclusao")))
            {
                property.SetColumnType("datetime");
                property.SetColumnName("InclusaoData");
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "UsuarioAlteracao")))
            {
                property.SetColumnType($"varchar(50)");
                property.SetColumnName("AlteracaoUsuario");
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "DataAlteracao")))
            {
                property.SetColumnType("datetime");
                property.SetColumnName("AlteracaoData");
            }
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataInclusao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataInclusao").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}