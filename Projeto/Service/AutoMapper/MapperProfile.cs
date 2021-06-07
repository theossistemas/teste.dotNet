using AutoMapper;
using Domain.Entity;
using Domain.Model.Book;

namespace Service.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookModel>();

            CreateMap<CreateBookModel, Book>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<Book, CreateBookModel>();

            CreateMap<Book, UpdateBookModel>();

            CreateMap<UpdateBookModel, Book>()
                .ForMember(target => target.Id, opt => opt.Ignore());

        }
    }
}
