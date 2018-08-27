using Dapper;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.QueryRepositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Livraria.Data.Dapper.Repositories
{
    public class LivroQueryRepository : QueryRepository<Livro>, ILivroQueryRepository
    {
        private const string SqlGetList = "SELECT L.*, A.*, E.* FROM [Livraria].[dbo].[Livro] L INNER JOIN [Livraria].[dbo].[Autor] A ON L.[AutorId] = A.[Id] INNER JOIN [Livraria].[dbo].[Editora] E ON L.[EditoraId] = E.[Id]";
        private const string SqlGetById = SqlGetList + " WHERE L.[Id] = @IdLivro";
        private const string SqlListarOrdenadoPorNome = "SELECT L.*, A.*, E.* FROM [Livraria].[dbo].[Livro] L INNER JOIN [Livraria].[dbo].[Autor] A ON L.[AutorId] = A.[Id] INNER JOIN [Livraria].[dbo].[Editora] E ON L.[EditoraId] = E.[Id] ORDER BY L.[Nome]";
        public LivroQueryRepository(SqlConnection connection) : base(connection, SqlGetById, SqlGetList)
        {
        }

        public IList<Livro> ListarOrdenadoPorNome()
        {
            return Connection.Query<Livro, Autor, Editora, Livro>(SqlListarOrdenadoPorNome, (l, a, e) =>
            {
                l.Autor = a;
                l.Editora = e;
                return l;
            }).ToList();
        }

        public override Livro GetById(string id)
        {
            return Connection.Query<Livro, Autor, Editora, Livro>(SqlGetById, (l, a, e) =>
            {
                l.Autor = a;
                l.Editora = e;
                return l;
            }, new { IdLivro = id }).FirstOrDefault();
        }

        public override IList<Livro> GetList()
        {
            return Connection.Query<Livro, Autor, Editora, Livro>(SqlGetList, (l, a, e) =>
               {
                   l.Autor = a;
                   l.Editora = e;
                   return l;
               }).ToList();
        }
    }
}
