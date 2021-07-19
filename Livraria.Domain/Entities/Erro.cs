using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Entities
{
    public class Erro
    {
        private readonly ILogger _logger;
        public Erro(ILogger<Erro> logger)
        {
            _logger = logger;
        }
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }

        public void OnGet()
        {
            Mensagem = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            Data = DateTime.Now;
            _logger.LogInformation(Mensagem);
        }
    }
}
