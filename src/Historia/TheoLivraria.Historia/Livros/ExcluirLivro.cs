using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheoLivraria.Dominio.Entidades;
using TheoLivraria.Dominio.IRepositories;

namespace TheoLivraria.Historia.Livros
{
    public class ExcluirLivro
    {
        public Dictionary<string, string> Erros { get; private set; } = new Dictionary<string, string>();
        private readonly ILivroRepository _livroRepository;

        public ExcluirLivro(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task Executar(Livro livro)
        {
            try
            {
                await _livroRepository.Excluir(livro);
            }
            catch (System.Exception)
            {
                this.Erros.Add("Erro", "Ocorreu um erro ao excluir um livro.");
            }

        }
    }
}
