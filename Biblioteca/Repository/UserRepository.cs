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
    public class UserRepository
    {
        public string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Entrevista\Teste.mdf;Integrated Security=True;Connect Timeout=30";

        public void Save(User model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("INSERT INTO USERS(Username,  FirstName, LastName, Permissao, PasswordHash,PasswordSalt) VALUES (@Username, @FirstName, @LastName, @Permissao, @PasswordHash,@PasswordSalt)", model);
            }
        }
        public void Update(User model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute(@"UPDATE USERS
                                       SET FirstName = @FirstName, 
                                           LastName = @LastName,
                                           City = @City,
                                           Permissao = @Permissao,
                                           PasswordHash = @PasswordHash,
                                           PasswordSalt = @PasswordSalt
                                       WHERE ID = @Id", model);
            }
        }

        public void Delete(User model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("DELETE FROM USERS WHERE ID = @Id", model);
            }
        }
        public List<User> GetUsers()
        {
            List<User> Users = new List<User>();

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<User>("Select *  from Users");

                foreach (User product in result)
                    Users.Add(product);
            }

            return Users;
        }

        public User GetUser(string username)
        {

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<User>($"Select * from Users where username = @username ", new {username}).SingleOrDefault();
            }
        }

        public User GetUserById(int userId)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<User>("Select * from Users where Id = @Id", new { Id = userId }).SingleOrDefault();
            }

        }

        public User ValidaUser(string username, string senha)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<User>($"Select * from Users where username = @username ", new { username }).SingleOrDefault();
            }

        }
    }
}
