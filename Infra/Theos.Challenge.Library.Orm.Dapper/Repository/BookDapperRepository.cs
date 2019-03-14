using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Theos.Challenge.Library.Model.Contracts.Infra;
using Theos.Challenge.Library.Orm.Dapper.Utils;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;
using System.Linq;

namespace Theos.Challenge.Library.Orm.Dapper.Repository
{    
    public sealed class BookDapperRepository : IBookRepository
    {        
        private readonly string getSqlConnectionString = DapperDbTools<Book>.dapperConnectionString;

        public async Task<int> Create(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Book>> GetAllBooks()
        {
            string query = "SELECT Identifier, Names, Title, Description, Subjects, Authors FROM BOOK";
            return await DapperDbTools<Book>.CreateQuery(query);
        }

        public async Task<Book> GetBookBy(Func<Book, bool> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookByIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}