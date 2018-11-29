using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LC.Persistence
{
    public class ACDbContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();

            //optionsBuilder.UseSqlServer(@"Data Source=CEDRONDS-034\SQLEXPRESS;Initial Catalog=LBDB;Integrated Security=True;Pooling=False");
            
            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
