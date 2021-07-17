using Livraria.Domain;
using Livraria.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Services.LivrosServices
{
    public class ConsultarLivro
    {
        private readonly ILivrosRepository _livroRepository;

        public ConsultarLivro(ILivrosRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public async Task<Livro> BuscarPorId(int id)
        {
            return await _livroRepository.BuscarPorId(id);
        }

        public async Task<Livro> BuscarPorNome(string nome)
        {
            return await _livroRepository.BuscarPorNome(nome);
        }

        public async Task<IEnumerable<Livro>> ListarTodos()
        {
            var AllLivros = await _livroRepository.ListarTodos();

            return AllLivros;
        }
    }
}
