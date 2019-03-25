using Theos.Library.Core.Data.Repository.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Filter.Book;
using Theos.Library.Domain.Books;

namespace Theos.Library.Core.Data.Repository.Interface
{
    public interface IBookRepository : IBaseRepository<Book, BookFilter>
    {
    }
}
