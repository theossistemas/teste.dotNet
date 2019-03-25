using Theos.Library.Core.Business.Base;
using Theos.Library.Core.Business.Interface;
using Theos.Library.Core.Data.Repository.Interface;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Filter.Book;
using Theos.Library.Domain.Books;

namespace Theos.Library.Core.Business
{
    public class BookService : BaseService<Book, BookFilter>, IBookService
    {
        public BookService(IBookRepository repository) : base(repository)
        {
        }
    }
}
