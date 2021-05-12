using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheoLivraria.Dominio.Entidades;
using TheoLivraria.WebAPI.Models;

namespace TheoLivraria.WebAPI.Factories
{
    public static class LivroFactory
    {
        public static LivroViewModel MapearLivroViewModel(Livro livro)
        {
            var livroViewModel = new LivroViewModel()
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Editora = livro.Editora
            };

            return livroViewModel;
        }
        public static Livro MapearLivro(LivroViewModel livroViewModel)
        {
            var livro = new Livro(livroViewModel.Id, livroViewModel.Nome, livroViewModel.Editora);

            return livro;
        }
        public static IEnumerable<LivroViewModel> MapearListaDeLivrosViewModel(IEnumerable<Livro> livros)
        {
            var lista = new List<LivroViewModel>();

            foreach (var item in livros)
            {
                lista.Add(MapearLivroViewModel(item));
            }
            return lista;
        }
    }
}
