using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.DTOs.DTOs.Livros
{
    public class AutorDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public LivroDTO? LivrosNomes { get; set; }
    }
}
