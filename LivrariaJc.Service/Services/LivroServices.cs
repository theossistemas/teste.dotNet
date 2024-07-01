using AutoMapper;
using LivrariaJc.Domain.Entidades;
using LivrariaJc.Domain.Imput;
using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Interfaces.Repositories;
using LivrariaJc.Domain.Interfaces.Services;
using LivrariaJc.Domain.Output;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LivrariaJc.Service.Services
{
    public class LivroServices : ILivroServices
    {
        private readonly ILivroRepositories _repositories;
        private readonly IMapper _mapper;

        public LivroServices(ILivroRepositories repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ServiceResult> ObterTodosAsync(LivroFilterInput input)
        {
            return new ServiceResult(await _repositories.ObterPaginadoAsync(input));
        }

        public async Task<ServiceResult> ObterAsync(int id)
        {
            var validacoes = await LocalizaCadastro(id);

            if (!validacoes)
                return new ServiceResult("Id", "Livro não encontrado!");

            return new ServiceResult(await _repositories.SelecionarAsync(id));
        }

        public async Task<ServiceResult> NovoAsync(LivroPostDto dto)
        {
            var validacoes = await ValidaNovoCadastro(dto);

            if (validacoes.Error.Any())
                return new ServiceResult(validacoes.Error);

            var livro = _mapper.Map<LivrosEntidade>(dto);

            return new ServiceResult(await _repositories.InserirAsync(livro));
        }

        public async Task<ServiceResult> AlterarAsync(LivroPutDto dto)
        {
            var validacoes = await ValidaAlteracaoCadastro(dto);

            if (validacoes.Error.Any())
                return new ServiceResult(validacoes.Error);

            var livro = _mapper.Map<LivrosEntidade>(dto);

            return new ServiceResult(await _repositories.AtualizarAsync(livro));
        }

        public async Task<ServiceResult> ExcluirAsync(int id)
        {
            if (!await LocalizaCadastro(id))
                return new ServiceResult("Id", "Livro não encontrado!");

            return new ServiceResult(await _repositories.ExcluirAsync(id));
        }

        private async Task<bool> LocalizaCadastro(int id)
        {
            if (!await _repositories.ExisteAsync(id))
                return false;

            return true;
        }

        private async Task<ServiceResult> ValidaNovoCadastro(LivroPostDto dto)
        {
            var validacoes = LivroPostDto.Validar(dto);

            if (!validacoes.IsValid)
            {
                var resultado = new ServiceResult(null);
                validacoes.Errors.ToList().ForEach(e => resultado.AdicionarErro(e.PropertyName, e.ErrorMessage));
                return resultado;
            }

            if (!await _repositories.VerificaLivroCadastrado(dto.Titulo.ToLower().Trim()))
                return new ServiceResult("Id", "Livro já cadastrado!");

            return new ServiceResult(true);
        }

        private async Task<ServiceResult> ValidaAlteracaoCadastro(LivroPutDto dto)
        {
            var validacoes = LivroPutDto.Validar(dto);

            if (!validacoes.IsValid)
            {
                var resultado = new ServiceResult(null);
                validacoes.Errors.ToList().ForEach(e => resultado.AdicionarErro(e.PropertyName, e.ErrorMessage));
                return resultado;
            }

            if (!await LocalizaCadastro(dto.Id))
                return new ServiceResult("Id", "Livro não encontrado!");

            if (await _repositories.VerificaLivroCadastrado(dto.Titulo.ToLower().Trim(), dto.Id))
                return new ServiceResult("Titulo", "Livro já cadastrado!");

            return new ServiceResult(true);
        }
    }
}
