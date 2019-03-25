using Theos.Library.Core.Business.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Filter.Book;
using Theos.Library.Domain.Books;

namespace Theos.Library.Core.Business.Interface
{
    public interface IBookService : IBaseService<Book, BookFilter>
    {
    }
}
