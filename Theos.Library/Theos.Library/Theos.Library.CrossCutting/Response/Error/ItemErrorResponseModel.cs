using System.Collections.Generic;

namespace Theos.Library.CrossCutting.Response.Error
{
    public class ItemErrorResponseModel
    {
        public ItemErrorResponseModel()
        {
        }

        public ItemErrorResponseModel(string field)
        {
            Field = field;
        }

        public ItemErrorResponseModel(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}
