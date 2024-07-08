using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.DTOs.DTOs.Livros
{
    public class CategoriaDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public LivroDTO? LivrosNomes { get; set; }
    }
}
