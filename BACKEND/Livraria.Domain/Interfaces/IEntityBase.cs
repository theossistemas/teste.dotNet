using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public abstract class IEntityBase
    {
        #region[Propriedades]
        public abstract DateTime CriadoEm { get; set; }
        public abstract DateTime? AtualizadoEm { get; set; }
        public abstract DateTime? ExcluidoEm { get; set; }
        public abstract bool Ativo { get; set; }
        #endregion
    }
}
