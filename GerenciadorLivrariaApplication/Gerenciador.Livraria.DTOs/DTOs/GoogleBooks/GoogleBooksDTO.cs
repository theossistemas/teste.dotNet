using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.DTOs.DTOs.GoogleBooks
{
    public class GoogleBooksResponse
    {
        public string Tipo { get; set; }
        public int QuantidadeDeResultados { get; set; }
        public IEnumerable<GoogleBooksDTO>? DadosDosResultados { get; set; }
    }

    public class GoogleBooksDTO
    {
        public string Id {  get; set; }
        public InformacoesDaObra? InformacoesDaObra { get; set; }
    }

    public class InformacoesDaObra
    {
        public string Titulo { get; set; }
        public List<string>? Autores { get; set; }
        public string? Editora { get; set; }
        public string? DataDePublicacao { get; set; }
        public string? Descricao { get; set; }
        public int? QuantidadeDePaginas { get; set; }
        public List<string>? Categorias { get; set; }
        public string? Idioma { get; set; }
    }
}
