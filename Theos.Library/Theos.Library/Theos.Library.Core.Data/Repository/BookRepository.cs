using System;
using System.Linq;
using Theos.Library.Core.Data.Context;
using Theos.Library.Core.Data.Repository.Base;
using Theos.Library.Core.Data.Repository.Interface;
using Theos.Library.CrossCutting.Filter.Book;
using Theos.Library.CrossCutting.Request;
using Theos.Library.Domain.Books;

namespace Theos.Library.Core.Data.Repository
{
    public class BookRepository : BaseRepository<Book, BookFilter, BookKey>, IBookRepository
    {
        protected override IQueryable<Book> Filter(RequestModel<BookFilter> request, IQueryable<Book> query, DataContext context)
        {
            if (!string.IsNullOrEmpty(request.Filter.Term))
            {
                var term = request.Filter.Term;
                query = query.Where(w => w.Author.ToUpper().Contains(term) || w.Description.ToUpper().Contains(term) || w.Title.ToUpper().Contains(term));
            }

            switch (request.Filter.Ordination)
            {
                case BookOrdination.Title:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                    break;
                case BookOrdination.Author:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Author) : query.OrderByDescending(o => o.Author);
                    break;
                case BookOrdination.Description:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Description) : query.OrderByDescending(o => o.Description);
                    break;
            }

            return base.Filter(request, query, context);
        }
    }
}
