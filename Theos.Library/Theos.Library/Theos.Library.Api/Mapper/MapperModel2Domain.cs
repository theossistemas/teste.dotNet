using AutoMapper;
using Theos.Library.Api.Models.Book;
using Theos.Library.CrossCutting;
using Theos.Library.CrossCutting.Helper;
using Theos.Library.Domain.Books;

namespace Theos.Library.Api.Mapper
{
    public class MapperModel2Domain : Profile
    {
        public MapperModel2Domain()
        {
            CreateMap<BookInsertModel, Book>()
                .ForMember(to => to.Image, map => map.MapFrom(src => UrlHelper.UrlDomainSimply(src.Cover)))
                ;

            CreateMap<BookUpdateModel, Book>()
                .ForMember(to => to.Image, map => map.MapFrom(src => UrlHelper.UrlDomainSimply(src.Cover)))
                ;
        }
    }
}
