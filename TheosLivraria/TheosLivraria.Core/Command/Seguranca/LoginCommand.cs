using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Command.Seguranca
{
    public record LoginCommand(string Email, string Senha);
}
