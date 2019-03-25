using AutoMapper;
using Theos.Library.Api.Models.Book;
using Theos.Library.CrossCutting.Helper;
using Theos.Library.Domain.Books;

namespace Theos.Library.Api.Mapper
{
    public class MapperDomain2Model : Profile
    {
        public MapperDomain2Model()
        {
            CreateMap<Book, BookListModel>()
                .ForMember(to => to.Cover, map => map.MapFrom(src => UrlHelper.UrlFormatter(src.Image)))
                ;

            CreateMap<Book, BookModel>()
                .ForMember(to => to.Cover, map => map.MapFrom(src => UrlHelper.UrlFormatter(src.Image)))
                ;
        }
    }
}
