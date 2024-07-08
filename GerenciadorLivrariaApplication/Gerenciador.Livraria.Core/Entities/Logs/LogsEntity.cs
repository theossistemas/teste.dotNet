using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Entities.Logs
{
    public class LogsEntity
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public string? TipoDeRegistro { get; set; }
        public DateTime DataDeRegistro { get; set; }
    }
}
