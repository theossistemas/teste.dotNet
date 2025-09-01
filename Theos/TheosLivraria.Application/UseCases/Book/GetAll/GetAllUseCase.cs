
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAll;
using TheosLivraria.Domain.Interfaces.Repository;

namespace TheosLivraria.Application.UseCases.Book.GetAll
{
    public class GetAllUseCase(IBookRepository bookRepository) : IGetAllUseCase
    {
        public async Task<List<ResponseBookAllViewModel>> ExecuteAsync()
        {
            var data = await bookRepository.GetAllAsync(x => x.Active == true);

            var viewModel = new ResponseBookAllViewModel().ModelToViewModel([.. data]);

            return viewModel;
        }
    }
}
