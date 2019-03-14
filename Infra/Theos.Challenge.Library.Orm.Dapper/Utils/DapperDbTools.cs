using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Theos.Challenge.Library.Orm.Dapper.Utils
{
    public class DapperDbTools<E> where E: class
    {
        private static SqlConnection _sqlConnection; 
        public static readonly string dapperConnectionString = @"Server=myServerName\\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;";        
        public static SqlConnection SqlConnection { get => _sqlConnection; private set => _sqlConnection = value; }

        private DapperDbTools() { }

        public static async Task<IList<E>> CreateQuery(string query)
        {   
            IList<E> resultados = new List<E>();

            using(SqlConnection = new SqlConnection(dapperConnectionString)){
                SqlConnection.Open();    
		        resultados = SqlConnection.Query<E>(query).ToList();    
		        SqlConnection.Close(); 
            }

            return resultados;
        }        
        public static int CreateDmlQuery(string queryDml, Dictionary<string,object> parameters)
        {
            using (var _sqlConnection = new SqlConnection(dapperConnectionString))    
            {    
                _sqlConnection.Open();    
                var affectedRows = _sqlConnection.Execute(queryDml,parameters );    
                _sqlConnection.Close();    
                return affectedRows;    
            }                
        }
    }
}