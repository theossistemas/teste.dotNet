using System.Collections.Generic;

namespace LivrariaJc.Domain.Output
{
    public class ServiceResult
    {
        public object Data { get; set; }
        public Dictionary<string, string> Error { get; set; } = new Dictionary<string, string>();

        public ServiceResult()
        {

        }

        public ServiceResult(Dictionary<string, string> error)
        {
            Error = error;
        }

        public ServiceResult(object result)
        {
            Data = result;
        }

        public ServiceResult(string campo, string message)
        {
            AdicionarErro(campo, message);
        }

        public ServiceResult(object data, Dictionary<string, string> error)
        {
            Data = data;
            Error = error;
        }

        public void AdicionarErro(string campo, string texto)
        {
            Error ??= new Dictionary<string, string>();
            Error.Add(campo, texto);
        }
    }
}
