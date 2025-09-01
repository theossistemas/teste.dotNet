
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;

namespace TheosLivraria.Application.UseCases.Book.Update
{
    public interface IUpdateBookUseCase
    {
        Task Execute(int Id, RequestUpdateBookViewModel request);
    }
}
