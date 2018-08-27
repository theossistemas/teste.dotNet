using AutoMapper;
using Livraria.Command;
using Livraria.Domain.Interface.QueryRepositories;
using Livraria.Service.DTOs;
using Livraria.Service.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace Livraria.Service.Classes
{
    public class LivroService : ILivroService
    {
        private readonly IMapper _mapper;
        private readonly ILivroQueryRepository _livroQueryRepository;
        private readonly IMediator _mediator;

        public LivroService(IMapper mapper, ILivroQueryRepository livroQueryRepository, IMediator mediator)
        {
            _mapper = mapper;
            _livroQueryRepository = livroQueryRepository;
            _mediator = mediator;
        }


        public void Alterar(LivroDTO livroDTO)
        {
            var command = _mapper.Map<AlterarLivroCommand>(livroDTO);
            _mediator.Send(command);
        }

        public LivroDTO Consultar(string id)
        {
            return _mapper.Map<LivroDTO>(_livroQueryRepository.GetById(id));
        }

        public void Excluir(string id)
        {
            var command = new ExcluirLivroCommand(id);
            _mediator.Send(command);
        }

        public void Incluir(LivroDTO livroDTO)
        {
            var command = _mapper.Map<IncluirLivroCommand>(livroDTO);
            _mediator.Send(command);
        }

        public IList<LivroDTO> Listar()
        {
            return _mapper.Map<IList<LivroDTO>>(_livroQueryRepository.GetList());
        }

        public IList<LivroDTO> ListarOrdenadoPorNome()
        {
            return _mapper.Map<IList<LivroDTO>>(_livroQueryRepository.ListarOrdenadoPorNome());
        }
    }
}
