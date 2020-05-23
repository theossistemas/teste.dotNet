using AutoMapper;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            string asdf = new ISBN("asdf");
            CreateMap<BookModel, Book>()
                .ForMember(entity => entity.YearPublication, opt =>
                    opt.MapFrom(model => model.Year));

            CreateMap<Book, BookModel>()
                .ForMember(model => model.ISBN, opt =>
                    opt.MapFrom(entity => entity.ISBN.Value))
                .ForMember(model => model.Year, opt =>
                    opt.MapFrom(entity => entity.YearPublication));

        }
    }
}
