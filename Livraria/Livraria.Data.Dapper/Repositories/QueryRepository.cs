using Dapper;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.QueryRepositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Livraria.Data.Dapper.Repositories
{
    public abstract class QueryRepository<T> : IQueryRepository<T> where T : EntidadeBase
    {
        protected readonly SqlConnection Connection;
        private readonly string _sqlGetById;
        private readonly string _sqlGetList;

        public QueryRepository(SqlConnection connection, string sqlGetById, string sqlGetList)
        {
            Connection = connection;
            _sqlGetById = sqlGetById;
            _sqlGetList = sqlGetList;
        }

        public virtual T GetById(string id)
        {
            return Connection.QueryFirstOrDefault<T>(_sqlGetById, new { Id = id });
        }

        public virtual IList<T> GetList()
        {
            return Connection.Query<T>(_sqlGetList).ToList();
        }
    }
}
