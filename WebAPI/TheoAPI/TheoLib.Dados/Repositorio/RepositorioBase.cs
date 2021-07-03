using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TheoLib.Dados.Repositorio
{
    public class RepositorioBase
    {
        private readonly IConfiguration _configuration;

        public RepositorioBase(IConfiguration configuration) => _configuration = configuration;

        protected string ObterConexaoBanco
        {
            get { return _configuration.GetConnectionString("stringBanco"); }
        }

        protected IDbConnection OpenConn() => new SqlConnection(ObterConexaoBanco);

    }
}
