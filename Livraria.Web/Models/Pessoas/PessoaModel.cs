using Livraria.Web.Models.Livros;

using System.Collections.Generic;

namespace Livraria.Web.Models.Pessoas
{
    public class PessoaModel
    {
        public string Nome { get; set; }

        public virtual ICollection<LivroModel> Livros { get; set; }
    }
}
