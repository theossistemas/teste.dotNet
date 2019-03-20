using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Impl.Context
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            //builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TesteSubwayDB;Trusted_Connection=True;");
            builder.UseSqlServer("Data Source=localhost;Initial Catalog=TesteDb; Trusted_Connection = True;");

            return new DataContext(builder.Options);
        }
    }
}
