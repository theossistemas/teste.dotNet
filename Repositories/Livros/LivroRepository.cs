using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Entities;
using Utils.Connection;
using Utils.Exceptions;
using Utils.Exceptions.Livro;

namespace Repositories.Livros
{
    public class LivroRepository : ILivroRepository
    {
        public void Delete(Int64? id)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    connection.Execute(@"DELETE FROM Livro WHERE Id = @id", new { id }, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex.GravarLog();
                }
            }
        }

        public Livro Find(Int64? id)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Livro>(@"SELECT * FROM Livro WHERE Id = @id", new { id })
                    .FirstOrDefault();
        }

        public IList<Livro> FindAll()
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Livro>(@"SELECT * FROM Livro ORDER BY Titulo ASC")
                    .ToList();
        }

        public Livro Save(Livro livro)
        {
            this.VerificarSeLivroNaoExiste(livro);

            using (IDbConnection connection = SqlServerHelper.Connection)
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    livro.Id = connection.Query<Int64>(@"PROC_SalvarAtualizarLivro", new
                    {
                        id = livro.Id,
                        titulo = livro.Titulo,
                        descricao = livro.Descricao
                    },
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

                    transaction.Commit();

                    return livro;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex.GravarLog();
                }
            }
        }

        public void VerificarSeLivroNaoExiste(Livro livro)
        {
            if (livro.Id == null)
                using (IDbConnection connection = SqlServerHelper.Connection)
                {
                    Boolean jaCadastrado = connection.Query<Int32>("SELECT COUNT(Id) FROM Livro WHERE LOWER(Titulo) = LOWER(@titulo)", new
                    {
                        titulo = livro.Titulo
                    }).FirstOrDefault() > 0;

                    if (jaCadastrado)
                        throw new LivroJaCadastradoException();
                }
        }

        public IList<Livro> FindByTitle(String title)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Livro>(@"SELECT * FROM Livro WHERE Titulo LIKE @titulo ORDER BY Titulo ASC", new
                { 
                    titulo = $"%{title}%"
                }).ToList();
        }
    }
}
