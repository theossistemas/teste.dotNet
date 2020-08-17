using System;
using System.Net;

namespace RestAPIClient.Response
{
    public interface IApiResponse
    {
        Object ResponseBody { get; set; }

        HttpStatusCode HttpStatusCode { get; set; }

        Boolean Error { get; set; }

        String ErrorMessage { get; set; }

        void SetError(Exception exception);
    }
}