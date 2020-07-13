using LibraryStore.Core.Data.Dtos;
using data = LibraryStore.Tests.Mocks.Datas.BookData;

namespace LibraryStore.Tests.Mocks.Dtos.Inputs
{
    public static class BookInputDtoMock
    {
        public static BookInputDto CreateBook1()
        {
            return new BookInputDto
            {
                Title = data.Title,
                Description = data.Description,
                Author = data.Author
            };
        }
    }
}