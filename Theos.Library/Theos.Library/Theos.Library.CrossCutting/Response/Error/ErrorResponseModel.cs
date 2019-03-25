using System.Collections.Generic;

namespace Theos.Library.CrossCutting.Response.Error
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel()
        {
            Message = "The given model is invalid.";
        }

        public ErrorResponseModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
        public List<ItemErrorResponseModel> Errors { get; set; }
    }
}
