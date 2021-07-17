using Livraria.Domain;
using Livraria.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Livraria.Services.LivrosServices
{
    public class CriarLivro
    {
        public Dictionary<string, string> 
            Erros { get; private set; } = new Dictionary<string, string>();


        private readonly ILivrosRepository _livrosRepository;

        public CriarLivro(ILivrosRepository livroRepository)
        {
            _livrosRepository = livroRepository;
        }

        public async Task Executar(Livro livro)
        {
            try
            {
                await _livrosRepository.Criar(livro);
            }
            catch (System.Exception)
            {
                this.Erros.Add("Erro", "Ocorreu um erro ao criar um livro.");
            }

        }
    }
}
