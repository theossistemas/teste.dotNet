
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.Create;

namespace TheosLivraria.Application.UseCases.Book.Create
{
    public interface ICreateBookUseCase
    {
        Task<ResponseCreateBookViewModel?> Execute(RequestCreateBookViewModel request);
    }
}
