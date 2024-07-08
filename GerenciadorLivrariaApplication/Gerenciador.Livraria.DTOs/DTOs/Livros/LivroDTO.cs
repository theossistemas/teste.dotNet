using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.DTOs.DTOs.Livros
{
    public class LivroDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public int AutorId { get; set; }
        public string? AutorNome { get; set; }
        public bool Ebook { get; set; } = false;
        public int CategoriaId { get; set; }
        public string? CategoriaNome { get; set; }
        public string? DataDePublicacao { get; set; }
    }
}
