using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Domain;
using Livraria.WebAPI;
using Livraria.WebAPI.DTO;

namespace Livraria.WebAPI.Factories
{
    public static class LivroFactory
    {
        public static LivroDTO MapearLivroDTO(Livro livro)
        {
            var livroDTO = new LivroDTO()
            {
                Id = livro.Id,
                Nome = livro.Nome
            };
            return livroDTO;
        }
        public static Livro MapearLivro(LivroDTO livroDTO)
        {
            var livro = new Livro(livroDTO.Id, livroDTO.Nome);

            return livro;
        }
        public static IEnumerable<LivroDTO> MapearListaDeLivrosDTO(IEnumerable<Livro> livros)
        {
            var lista = new List<LivroDTO>();

            foreach (var item in livros)
            {
                lista.Add(MapearLivroDTO(item));
            }
            return lista;
        }
    }
}
