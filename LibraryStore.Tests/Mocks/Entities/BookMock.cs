using LibraryStore.Core.Data.Entities;
using LibraryStore.Tests.Extensions;
using data = LibraryStore.Tests.Mocks.Datas.BookData;

namespace LibraryStore.Tests.Mocks.Entities
{
    public static class BookMock
    {
        public static Book CreateBook1()
        {
            var entidade = CreateInputBook1();
            entidade.Id = data.Id.ToGuid();
            entidade.CreatedAt = data.CreatedAt.ToDateTime();
            entidade.Active = data.Active;
            return entidade;
        }

        public static Book CreateInputBook1()
        {
            return new Book
            {
                Title = data.Title,
                Description = data.Description,
                Author = data.Author
            };
        }
    }
}