using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaTheos.Estoque.Data
{
    public static class Startup
    {
        public static void RunMigration<T>(IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>();
                using (var conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
                {
                    conn.Open();
                    context.Database.Migrate();
                    conn.Close();
                }
            }
        }
    }
}
