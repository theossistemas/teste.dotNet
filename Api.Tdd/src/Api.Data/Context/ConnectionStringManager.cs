using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data.Context
{
    public class ConnectionStringManager
    {
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
    }
}
