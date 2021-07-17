using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain
{
    public class Livro
    {
        public Livro(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public void AtualizarDadosDoLivro(string nome)
        {
            this.Nome = nome;
        }
    }
}
