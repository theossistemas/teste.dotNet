using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Domain;
using Livraria.WebAPI;
using Livraria.WebAPI.DTO;

namespace Livraria.WebAPI.Mapper
{
    public static class LivroMapper
    {
        public static LivroDTO MapperLivroDTO(Livro livro)
        {
            var livroDTO = new LivroDTO()
            {
                Id = livro.Id,
                Nome = livro.Nome
            };
            return livroDTO;
        }
        public static Livro MapperLivro(LivroDTO livroDTO)
        {
            var livro = new Livro(livroDTO.Id, livroDTO.Nome);

            return livro;
        }
        public static IEnumerable<LivroDTO> MapperListaDeLivrosDTO(IEnumerable<Livro> livros)
        {
            var lista = new List<LivroDTO>();

            foreach (var item in livros)
            {
                lista.Add(MapperLivroDTO(item));
            }
            return lista;
        }
    }
}
