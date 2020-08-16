using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Utils.Connection
{
    public class SqlServerHelper
    {
        private SqlServerHelper()
        { }

        public static SqlConnectionStringBuilder ConnectionStringBuilder { get; private set; }

        public static DbConnection Connection
        {
            get 
            {
                SqlConnection conn = new SqlConnection(ConnectionStringBuilder.ConnectionString);

                conn.Open();

                return conn;
            }
        }

        public static void Initializer(String connectionString)
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        public static void Initializer(SqlConnectionStringBuilder connectionStringBuilder)
        {
            ConnectionStringBuilder = connectionStringBuilder;
        }
    }
}
