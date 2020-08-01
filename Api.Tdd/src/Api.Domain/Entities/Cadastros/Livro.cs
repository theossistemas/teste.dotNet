using System;

namespace Api.Domain.Entities
{
    public class Livro : BaseEntity
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public string Emissora { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
