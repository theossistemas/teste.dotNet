using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Model.Contracts.Infra
{
    public interface IBookRepository
    {
         Task<int> Create(Book book);
         Task<Book> GetBookByIdentifier(Identifier identifier);
         Task<Book> GetBookBy(Func<Book,bool> condition);
         Task<IList<Book>> GetAllBooks();
         Task<bool> Update(Book book);
         Task<bool> Delete(Book book);
    }
}