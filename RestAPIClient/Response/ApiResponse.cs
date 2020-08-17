using System;
using System.Net;

namespace RestAPIClient.Response
{
    public class ApiResponse : IApiResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public String ErrorMessage { get; set; }

        public Object ResponseBody { get; set; }

        public Boolean Error { get; set; }

        public static ApiResponse Create()
        {
            return new ApiResponse();
        }

        public void SetError(Exception exception)
        {
            this.Error = true;
            this.ErrorMessage = exception.Message;
        }
    }
}
