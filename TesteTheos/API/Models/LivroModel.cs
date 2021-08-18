using System;
using System.Text.Json.Serialization;

namespace TesteTheos.API.Models
{
    public class LivroModel : LivroDetalhesViewModel
    {
        [JsonIgnore]
        new public Guid Id { get; set; }
    }
}
