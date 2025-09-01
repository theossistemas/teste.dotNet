
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook
{
    public class RequestCreateBookViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public BookEntity ViewModelToModel(RequestCreateBookViewModel request)
        {
            return new BookEntity
            {
                Name = request.Name,
                Author = request.Author
            };
        }

    }
}
