using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Services.Dto;
using Livraria.Services.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Test.ServicesMock.Cadastros
{
    public class LivroServiceAggregateExceptionMock : ILivroService
    {
        public Task<ResponseDto> AlterarLivro(LivroDto livroDto, int codigo)
        {
            var exceptions = new List<Exception>
            {
                new Exception("Exceção agregada 1"),
                new Exception("Exceção agregada 2"),
            };

            throw new AggregateException("Teste aggregate exceptions AlterarLivro", exceptions);
        }

        public Task<Livro> ConsultarPorId(int id)
        {
            var exceptions = new List<Exception>
            {
                new Exception("Exceção agregada 1"),
                new Exception("Exceção agregada 2"),
            };

            throw new AggregateException("Teste aggregate exceptions ConsultarPorId", exceptions);
        }

        public Task<List<Livro>> ConsultarTodosOrderByAsc()
        {
            var exceptions = new List<Exception>
            {
                new Exception("Exceção agregada 1"),
                new Exception("Exceção agregada 2"),
            };

            throw new AggregateException("Teste aggregate exceptions ConsultarTodosOrderByAsc", exceptions);
        }

        public Task<ResponseDto> ExcluirLivro(int codigo)
        {
            var exceptions = new List<Exception>
            {
                new Exception("Exceção agregada 1"),
                new Exception("Exceção agregada 2"),
            };

            throw new AggregateException("Teste aggregate exceptions ExcluirLivro", exceptions);
        }

        public Task Inserir(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> InserirNovo(Livro livro)
        {
            var exceptions = new List<Exception>
            {
                new Exception("Exceção agregada 1"),
                new Exception("Exceção agregada 2"),
            };

            throw new AggregateException("Teste aggregate exceptions InserirNovo", exceptions);
        }
    }
}
