using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ILivro
    {
        #region[Propriedades]
        int Id { get; set; }
        string Titulo { get; set; }
        string Autor { get; set; }
        string ISBN { get; set; } // código ISBN (identificação única no mercado)
        int AnoPublicacao { get; set; }
        int Quantidade { get; set; }
        decimal Preco { get; set; }
        #endregion
    }
}
