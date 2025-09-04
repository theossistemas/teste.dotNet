using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Command.Usuarios
{
    public record CriarUsuarioCommand(string Nome, string Documento, string Email, string Senha, string Telefone, DateTime DataAniversario, int Perfil);
}
