using MMM.Library.Domain.CQRS.Queries.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Queries
{
    public interface IBookQueries
    {
        Task<IEnumerable<BookAndCategoryViewModel>> GetAllBooksWithCategory();
    }
}
