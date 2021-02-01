using Newtonsoft.Json;

namespace Theos.Livraria.Domain.Model
{
    public class BaseResponse
    {
        [JsonIgnore]
        public int StatusCode { get; set; }  
         
        public string Mensagem { get; set; }

        public dynamic Conteudo { get; set; }
    }
}