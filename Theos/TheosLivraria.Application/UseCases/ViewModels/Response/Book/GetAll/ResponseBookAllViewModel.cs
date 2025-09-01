
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;

namespace TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAll
{
    public class ResponseBookAllViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifedDate { get; set; }

        public List<ResponseBookAllViewModel> ModelToViewModel(List<Domain.Models.Entities.BookEntity> books)
        {
            if (books == null) return [];

            return [.. books.Select(book => new ResponseBookAllViewModel
            {
                Id = book.Id,
                Nome = book.Name,
                Author = book.Author,
                CreatedDate = book.CreatedDate,
                ModifedDate = book.ModifiedDate
            })];

        }
    }
}
