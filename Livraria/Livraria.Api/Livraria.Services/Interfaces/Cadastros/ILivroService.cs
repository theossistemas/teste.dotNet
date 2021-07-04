using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Services.Interfaces.Cadastros
{
    public interface ILivroService
    {
        Task Inserir(Livro livro);
        Task<ResponseDto> InserirNovo(Livro livro);
        Task<List<Livro>> ConsultarTodosOrderByAsc();
        Task<Livro> ConsultarPorId(int id);
        Task<ResponseDto> AlterarLivro(LivroDto livroDto, int codigo);
        Task<ResponseDto> ExcluirLivro(int codigo);
    }
}
