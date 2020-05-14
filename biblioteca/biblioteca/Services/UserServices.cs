using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using biblioteca.models;


namespace biblioteca.Services
{
    public class UserServices
    {
        public User GetUserByNamePassword(User user, IConfiguration config)
        {
            using (SqlConnection conexao = new SqlConnection(
            config.GetConnectionString("Library")))
            {
                return conexao.Get<User>(user.id);
            }
        }
    }
}