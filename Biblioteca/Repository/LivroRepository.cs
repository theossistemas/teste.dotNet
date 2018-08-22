using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace WebApi.Repository
{
    public class LivroRepository
    {
        public string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Entrevista\Teste.mdf;Integrated Security=True;Connect Timeout=30";

        public void Save(Livro model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("INSERT INTO Livros(Autor,  Nome, Ano, DataFabricacao) VALUES (@Autor, @Nome, @Ano, @DataFabricacao)", model);
            }
        }
        public void Update(Livro model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute(@"UPDATE Livros
                                       SET Nome = @Nome, 
                                           Autor = @Autor,
                                           Ano = @Ano,
                                           DataFabricacao = @DataFabricacao
                                       WHERE ID = @Id", model);
            }
        }

        public void Delete(long id)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("DELETE FROM Livros WHERE ID = @Id", id);
            }
        }
        public List<Livro> GetLivros()
        {
            List<Livro> Livros = new List<Livro>();

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<Livro>("Select * from Livros");

                foreach (Livro livro in result)
                    Livros.Add(livro);
            }

            return Livros;
        }

        public Livro GetLivroNome(string nome)
        {

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<Livro>($"Select * from Livros where Nome = @nome ", new { nome }).SingleOrDefault();
            }
        }

        public Livro GetLivroById(long Id)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<Livro>("Select * from Livros where Id = @Id", new { Id }).SingleOrDefault();
            }

        }

        public Livro ValidaLivro(string username, string senha)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<Livro>($"Select * from Users where username = @username ", new { username }).SingleOrDefault();
            }

        }
    }
}
