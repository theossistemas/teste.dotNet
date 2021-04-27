using Microsoft.EntityFrameworkCore;
using teste.dotNet.API.Entities;

namespace teste.dotNet.API.Data {
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Writers {get; set;}
    }
}