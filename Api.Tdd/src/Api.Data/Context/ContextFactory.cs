using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{   
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {        
        public MyContext CreateDbContext (string[] args) {
            var con = new ConnectionStringManager();
            var connectionString = con.GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<MyContext> ();
            optionsBuilder.UseSqlServer (connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
