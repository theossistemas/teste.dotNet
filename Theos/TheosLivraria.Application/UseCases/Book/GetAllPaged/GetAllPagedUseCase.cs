using TheosLivraria.Application.UseCases.ViewModels.Common.Response;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UseCases;

namespace TheosLivraria.Application.UseCases.Book.GetAllPaged
{
    public class GetAllPagedUseCase(IBookRepository bookRepository) : IGetAllPagedUseCase
    {
        public async Task<ResponsePagedViewModel<ResponseBookAllPagedViewModel>> ExecuteAsync(RequestBookAllPagedViewModel request)
        {
            var  filter = request.ToFilter();

            var (books, count) = await bookRepository.GetBooksPagedAsync(filter);

            var booksViewModel = new ResponseBookAllPagedViewModel().ModelToViewModel(books);

            return new ResponsePagedViewModel<ResponseBookAllPagedViewModel>(booksViewModel, count, filter.Page, filter.Size);

        }


    }
}
