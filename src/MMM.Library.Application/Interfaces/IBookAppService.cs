using MMM.Library.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Interfaces
{
    public interface IBookAppService
    {
        Task<BookViewModel> GetById(Guid id);
        Task<IEnumerable<BookViewModel>> GetAll();
        Task<IEnumerable<BookViewModel>> GetByCategory(int code);

        Task AddBook(BookViewModel bookViewModel);
        Task UpdateBook(BookViewModel bookViewModel);
        Task DeleteBook(Guid id);
    }
}
