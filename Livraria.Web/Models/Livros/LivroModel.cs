using Livraria.Domain.Pessoas;

using System.Collections.Generic;

namespace Livraria.Web.Models.Livros
{
    public class LivroModel
    {
        public string Titulo { get; set; }
        public string Setor { get; set; }

        public virtual ICollection<string> Temas { get; set; }
        public virtual ICollection<Pessoa> Autores { get; set; }
    }
}
