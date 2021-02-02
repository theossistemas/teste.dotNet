using JeffersonMello.Livraria.Data.Configuration;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Data.Context
{
    public class DataContext : DbContext
    {
        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new LivroConfiguration());
        }

        #endregion Protected Methods

        #region Public Properties

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Livro> Livros { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DataContext(string connectionString)
          : base(new DbContextOptionsBuilder<DataContext>().UseSqlServer(connectionString).Options)
        {
        }

        #endregion Public Constructors
    }
}