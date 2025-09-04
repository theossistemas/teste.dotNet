using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Core.Sergurancao
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
