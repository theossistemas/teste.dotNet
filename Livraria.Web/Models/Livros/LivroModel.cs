using Livraria.Domain.Pessoas;
using Livraria.Web.Models.Pessoas;

using System.Collections.Generic;

namespace Livraria.Web.Models.Livros
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Setor { get; set; }

        public virtual ICollection<string> Temas { get; set; }
        public virtual ICollection<PessoaModel> Autores { get; set; }
    }
}
