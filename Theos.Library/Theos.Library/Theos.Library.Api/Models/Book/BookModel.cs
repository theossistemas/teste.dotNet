using Theos.Library.Api.Models.Base;

namespace Theos.Library.Api.Models.Book
{
    public class BookModel : BaseUpdateModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
    }
}
