using AutoMapper;
using LivrariaJc.Domain.Entidades;
using LivrariaJc.Domain.Imput;

namespace LivrariaJc.Data.Mapper
{
    public class LivroMapper : Profile
    {
        public LivroMapper()
        {
            CreateMap<LivroPostDto, LivrosEntidade>();
            CreateMap<LivroPutDto, LivrosEntidade>();
        }
    }
}
