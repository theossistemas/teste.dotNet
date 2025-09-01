namespace TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged
{
    public class ResponseBookAllPagedViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Author { get; set; }

        public List<ResponseBookAllPagedViewModel> ModelToViewModel(List<Domain.Models.Entities.BookEntity> books)
        {
            if (books == null) return [];

            return [.. books.Select(book => new ResponseBookAllPagedViewModel
            {
                Id = book.Id,
                Nome = book.Name,
                Author = book.Author
            })];

        }
    }
}
