using TheosLivraria.Domain.Models.Filters.Book;

namespace TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged
{
    public class RequestBookAllPagedViewModel
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public BookAllPagedFilter ToFilter()
        {
            return new BookAllPagedFilter
            {
                Page = Page,
                Size = Size
            };
        }
    }
}
