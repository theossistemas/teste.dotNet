using AutoMapper;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers.Profiles
{
    public class BookAuthorProfile : Profile
    {
        public BookAuthorProfile()
        {
            CreateMap<BookAuthor, AuthorModel>();
            CreateMap<AuthorModel, BookAuthor>();
        }
    }
}
