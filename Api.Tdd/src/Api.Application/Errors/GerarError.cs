using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Errors
{
    public class GerarError
    {
        public string Gerar(string Title, int? status, string Detail)
        {
            var problemDetails = new ProblemDetails();        

            problemDetails.Title = Title;
            problemDetails.Status = status;
            problemDetails.Detail = Detail;
            var json = JsonConvert.SerializeObject(problemDetails);            
            return  json;
        }
    }
}
