using Newtonsoft.Json;

namespace MaiaraBookstore.Models.DTO
{
    public class LivroDTO
    {
        [JsonProperty(PropertyName = "Titulo")]
        public string Titulo { get; set; }
    }
}
