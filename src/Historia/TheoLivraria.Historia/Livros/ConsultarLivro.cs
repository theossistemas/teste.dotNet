using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheoLivraria.Dominio.Entidades;
using TheoLivraria.Dominio.IRepositories;

namespace TheoLivraria.Historia.Livros
{
    public class ConsultarLivro
    {
        private readonly ILivroRepository _livroRepository;

        public ConsultarLivro(ILivroRepository livroRepository)
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
            var imoveis = await _livroRepository.ListarTodos();

            return imoveis;
        }
    }
}
