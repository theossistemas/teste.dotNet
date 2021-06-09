using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TMSA.Livraria.Infra.Data.Connection
{
    public abstract class ConnectionDB
    {

        private readonly IConfiguration _configuration;

        protected ConnectionDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("LivrariaDB"));
            }
        }
    }
}
