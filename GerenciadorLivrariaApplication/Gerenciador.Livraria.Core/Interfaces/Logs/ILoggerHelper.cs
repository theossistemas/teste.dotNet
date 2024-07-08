using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Interfaces.Logs
{
    public interface ILoggerHelper
    {
        Task RegistrarLog(string descricao, string tipoDeRegistro);
        Task RegistrarLogDeErro(Exception exception);
    }
}
