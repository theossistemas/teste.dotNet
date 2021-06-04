using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDotNet.Business.Models
{
    public class Livro: Entity
    {
        public string Nome { get; set; } 
        public string Categoria { get; set; }
        public string Autor { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Edicao { get; set; }

    }
}
