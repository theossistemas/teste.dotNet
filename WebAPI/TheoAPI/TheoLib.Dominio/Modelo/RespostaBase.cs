using Newtonsoft.Json;

namespace TheoLib.Dominio.Modelo
{
    public class RespostaBase
    {
        [JsonIgnore]
        public int StatusCode { get; set; }  
         
        public string Mensagem { get; set; }

        public dynamic Conteudo { get; set; }
    }
}
