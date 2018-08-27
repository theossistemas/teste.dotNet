using AutoMapper;
using Livraria.Command;
using Livraria.Domain.Entity;
using Livraria.Service.DTOs;

namespace Livraria.Service.AutoMapper
{
    internal class DTOToEntityProfile : Profile
    {
        public DTOToEntityProfile()
        {
            CreateMap<LivroDTO, IncluirLivroCommand>().ConstructUsing(x =>
            {
                var autor = new Autor(x?.Autor?.Id, x?.Autor?.Nome);
                var editora = new Editora(x?.Editora?.Id, x?.Editora?.Nome);
                var command = new IncluirLivroCommand(x.Nome, x.Descricao, autor, editora, x.Edicao);
                return command;
            });

            CreateMap<LivroDTO, AlterarLivroCommand>().ConstructUsing(x =>
            {
                var autor = new Autor(x?.Autor?.Id, x?.Autor?.Nome);
                var editora = new Editora(x?.Editora?.Id, x?.Editora?.Nome);
                var command = new AlterarLivroCommand(x.Id, x.Nome, x.Descricao, autor, editora, x.Edicao);
                return command;
            });

        }
    }
}