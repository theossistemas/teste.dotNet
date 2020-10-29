using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Connection
{
    public class ApplicationContext : DbContext
    {
        #region Properties
        public DbSet<Livros> Livros { get; set; }
        #endregion

        #region Constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
