using Api.Domain.Entities;
using AutoMapper;
using Domain.Dtos;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

            CreateMap<LivroDto, Livro>()
           .ReverseMap();

        }
    }
}
