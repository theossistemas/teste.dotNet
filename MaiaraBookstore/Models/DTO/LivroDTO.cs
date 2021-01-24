using Newtonsoft.Json;

namespace MaiaraBookstore.Models.DTO
{
    public class LivroDTO
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Titulo")]
        public string Titulo { get; set; }
    }
}
