using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Services.Dto;
using Livraria.Services.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Test.ServicesMock.Cadastros
{
    public class LivroServiceExceptionMock : ILivroService
    {
        public Task<ResponseDto> AlterarLivro(LivroDto livroDto, int codigo)
        {
            throw new Exception("Teste Exception AlterarLivro");
        }

        public Task<Livro> ConsultarPorId(int id)
        {
            throw new Exception("Teste Exception ConsultarPorId");
        }

        public Task<List<Livro>> ConsultarTodosOrderByAsc()
        {
            throw new Exception("Teste Exception ConsultarTodosOrderByAsc");
        }

        public Task<ResponseDto> ExcluirLivro(int codigo)
        {
            throw new Exception("Teste Exception ExcluirLivro");
        }

        public Task Inserir(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> InserirNovo(Livro livro)
        {

            throw new Exception("Teste Exception InserirNovo");
        }
    }
}
