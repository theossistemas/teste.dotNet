using LibraryStore.Core.Data.Dtos;
using LibraryStore.Tests.Extensions;
using data = LibraryStore.Tests.Mocks.Datas.BookData;

namespace LibraryStore.Tests.Mocks.Dtos
{
    public static class BookDtoMock
    {
        public static BookDto CreateBook1()
        {
            return new BookDto
            {
                Id = data.Id.ToGuid(),
                Title = data.Title,
                Description = data.Description,
                Author = data.Author,
                CreatedAt = data.CreatedAt.ToDateTime(),
                Active = data.Active
            };
        }
    }
}