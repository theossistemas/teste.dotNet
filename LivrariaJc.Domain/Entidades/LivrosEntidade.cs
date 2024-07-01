
using System;

namespace LivrariaJc.Domain.Entidades
{
    public class LivrosEntidade : BaseEntidade
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataLancamento { get; set; }
    }
}
