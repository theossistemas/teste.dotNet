
namespace TheosLivraria.Application.UseCases.ViewModels.Common.Response
{
    public class ResponsePagedViewModel<T> where T : class
    {
        public ResponsePagedViewModel(List<T> items, int count, int page, int size)
        {
            Items = items;
            TotalCount = count;
            Page = page;
            Size = size;
        }
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
