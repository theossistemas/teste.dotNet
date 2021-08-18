using System.ComponentModel.DataAnnotations;

namespace TesteTheos.API.Models
{
    public class LivroDetalhesViewModel : LivroViewModel
    {
        [MaxLength(1500)]
        public string Sinopse { get; set; }
    }
}
