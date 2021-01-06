using AutoMapper;

using Livraria.Domain.Livros;
using Livraria.Domain.Pessoas;
using Livraria.Domain.Usuarios;
using Livraria.Web.Models.Livros;
using Livraria.Web.Models.Pessoas;
using Livraria.Web.Models.Usuarios;

using System.Linq;

namespace Livraria.Web
{
    internal class WebAutoMapperConfig : Profile
    {
        public WebAutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(
                dest => dest.Senha,
                dest => dest.Ignore());

            CreateMap<UsuarioModel, Usuario>();

            CreateMap<Pessoa, PessoaModel>()
                .ForMember(
                dest => dest.Livros,
                dest => dest.Ignore());
            CreateMap<PessoaModel, Pessoa>();

            CreateMap<Livro, LivroModel>()
                .ForMember(
                dest => dest.Temas,
                opt => opt.MapFrom(
                    src => src.Temas.Select(
                        t => t.Tema.Valor)))   
                .ForMember(
                dest => dest.Autores,
                opt => opt.MapFrom(
                    src => src.Autores.Select(
                        t => t.Autor)));
            CreateMap<LivroModel, Livro>()
                .ForMember(dest => dest.Temas, dest => dest.Ignore())
                .ForMember(dest => dest.Autores, dest => dest.Ignore());
        }
    }
}