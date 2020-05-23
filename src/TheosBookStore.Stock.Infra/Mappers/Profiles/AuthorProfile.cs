using AutoMapper;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();
        }
    }
}
