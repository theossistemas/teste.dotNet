using AutoMapper;
using LivrariaTheos.Estoque.Domain.Livros.Dto;
using LivrariaTheos.Estoque.Domain.Livros.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Application.Services
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

        public LivroAppService(ILivroRepository livroRepository,
             IMapper mapper)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
        }

        public async Task<LivroDtoRetorno> ObterPorId(int id)
        {
            return _mapper.Map<LivroDtoRetorno>(await _livroRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterTodos());
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterTodosAtivos()
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterTodosAtivos());
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterPorAutor(int autorId)
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterPorAutor(autorId));
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterPorGenero(int generoId)
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterPorGenero(generoId));
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterPorNome(string nome)
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterPorNome(nome));
        }

        public async Task<IEnumerable<LivroDtoRetorno>> ObterPorNacionalidadeDoAutor(int nacionalidade)
        {
            return _mapper.Map<IEnumerable<LivroDtoRetorno>>(await _livroRepository.ObterPorNacionalidadeDoAutor(nacionalidade));
        }        

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}