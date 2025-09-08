using Livraria.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<LivrariaDbContext>
{
    public LivrariaDbContext CreateDbContext(string[] args)
    {
        // caminho até o appsettings.json da API
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Livraria");

        if (basePath == null)
        {
            throw new InvalidOperationException("Não foi possível determinar o caminho base para o arquivo de configuração.");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<LivrariaDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new LivrariaDbContext(optionsBuilder.Options);
    }
}
