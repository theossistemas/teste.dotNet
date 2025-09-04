using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Command.Usuarios
{
   public record AlterarUsuarioCommand(Guid Id, string Nome, string Documento, string Email, string Telefone, DateTime DataAniversario);
}
