using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Challenge.Library.Model.Contracts.Infra;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Orm.Dapper.Repository
{
    public sealed class BookRepository : IBookRepository
    {
        public Task<int> Create(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookBy(Func<Book, bool> condition)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookByIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}