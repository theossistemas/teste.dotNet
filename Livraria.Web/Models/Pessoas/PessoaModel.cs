using Livraria.Web.Models.Livros;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Web.Models.Pessoas
{
    public class PessoaModel
    {
        public string Nome { get; set; }

        public virtual ICollection<LivroModel> Livros { get; set; }
    }
}
