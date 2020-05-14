using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace biblioteca.Services
{

    public class BookServices
    {
        public IEnumerable<Book> GetAllBooks(IConfiguration config)
        {
            using (SqlConnection conexao = new SqlConnection(
            config.GetConnectionString("Library")))
            {
                return conexao.Query<Book>("select * from livros order by nome asc");
            }
        }
        public JsonResult AddBook(Book newBook, IConfiguration config)
        {
            using (SqlConnection conexao = new SqlConnection(
                            config.GetConnectionString("Library")))
            {
                Guid g = Guid.NewGuid();
                newBook.id = g.ToString();
                return new JsonResult(conexao.Insert(newBook));
            }
        }

        public JsonResult UpdteBook(Book modifyBook, IConfiguration config)
        {
            using (SqlConnection conexao = new SqlConnection(
                            config.GetConnectionString("Library")))
            {
                return new JsonResult(conexao.Update(modifyBook));
            }
        }

        public JsonResult RemoveBook(Book book, IConfiguration config)
        {
            using (SqlConnection conexao = new SqlConnection(
                            config.GetConnectionString("Library")))
            {
                return new JsonResult(conexao.Delete(book));
            }
        }


    }
}
