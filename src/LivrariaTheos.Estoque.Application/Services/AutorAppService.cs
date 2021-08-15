using AutoMapper;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Autores.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public class AutorAppService : IAutorAppService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorAppService(IAutorRepository autorRepository,
             IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
        }

        public async Task<AutorDto> ObterPorId(int id)
        {
            return _mapper.Map<AutorDto>(await _autorRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<AutorDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.ObterTodos());
        }

        public async Task<IEnumerable<AutorDto>> ObterTodosAtivos()
        {
            return _mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.ObterTodosAtivos());
        }

        public async Task<IEnumerable<AutorDto>> ObterPorNome(string nome)
        {
            return _mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.ObterPorNome(nome));
        }

        public async Task<IEnumerable<AutorDto>> ObterPorNacionalidade(int nacionalidade)
        {
            return _mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.ObterPorNacionalidade(nacionalidade));
        }        

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }
    }
}