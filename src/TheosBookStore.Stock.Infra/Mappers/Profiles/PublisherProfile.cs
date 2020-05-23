using AutoMapper;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherModel>();
            CreateMap<PublisherModel, Publisher>();
        }
    }
}
