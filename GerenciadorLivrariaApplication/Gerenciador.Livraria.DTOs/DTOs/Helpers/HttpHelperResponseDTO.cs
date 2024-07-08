using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.DTOs.DTOs.Helpers
{
    public class HttpHelperResponseDTO
    {
        public string? Mensagem { get; set; }
        public HttpStatusCode CodigoDeStatus { get; set; }
        public string? Dados { get; set; }
    }
}
