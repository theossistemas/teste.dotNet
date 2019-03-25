namespace Theos.Library.CrossCutting.Response.Error
{
    public class InternalServerErrorResponseModel
    {
        public InternalServerErrorResponseModel()
        {
            Message = "An error has occurred, try again later or contact technical support.";
        }

        public InternalServerErrorResponseModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

    }
}
