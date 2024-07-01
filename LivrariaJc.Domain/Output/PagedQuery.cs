using System.Collections.Generic;

namespace LivrariaJc.Domain.Output
{
    public class PagedQuery<T> where T : class
    {
        public int PaginaAtual { get; set; }
        public int PaginaTotal { get; set; }
        public int TamanhoPagina { get; set; }
        public IEnumerable<T> Dados { get; set; }
    }
}
