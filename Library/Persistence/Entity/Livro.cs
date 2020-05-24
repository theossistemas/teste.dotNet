using Persistence.Entity.Base;

namespace Persistence.Entity
{
    public class Livro : EntityBase
    {
        public string Descricao { get; set; }
        public string Ano { get; set; }
        public string Autor { get; set; }
    }
}
