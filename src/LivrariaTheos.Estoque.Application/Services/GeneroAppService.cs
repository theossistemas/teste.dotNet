using AutoMapper;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Generos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public class GeneroAppService : IGeneroAppService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroAppService(IGeneroRepository generoRepository,
             IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }       

        public async Task<GeneroDto> ObterPorId(int id)
        {
            return _mapper.Map<GeneroDto>(await _generoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<GeneroDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GeneroDto>>(await _generoRepository.ObterTodos());
        }

        public async Task<IEnumerable<GeneroDto>> ObterTodosAtivos()
        {
            return _mapper.Map<IEnumerable<GeneroDto>>(await _generoRepository.ObterTodosAtivos());
        }

        public async Task<IEnumerable<GeneroDto>> ObterPorNome(string nome)
        {
            return _mapper.Map<IEnumerable<GeneroDto>>(await _generoRepository.ObterPorNome(nome));
        }        

        public void Dispose()
        {
            _generoRepository?.Dispose();           
        }
    }
}
