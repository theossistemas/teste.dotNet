using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Teste.Domain.Repositories
{
    public interface IAuthorBookRepository : IRepository<AuthorBook>
    {
        IEnumerable<AuthorBook> getAllBook();
    }
}