using LibraryStore.Core.Data.Entities;
using LibraryStore.Tests.Extensions;
using data = LibraryStore.Tests.Mocks.Datas.UserData;

namespace LibraryStore.Tests.Mocks.Entities
{
    public static class UserMock
    {
        public static User CreateAdmin()
        {
            var entidade = CreateInputAdmin();
            entidade.Id = data.Id.ToGuid();
            entidade.CreatedAt = data.CreatedAt.ToDateTime();
            entidade.Active = data.Active;
            return entidade;
        }

        public static User CreateInputAdmin()
        {
            return new User
            {
                Fullname = data.Fullname,
                Username = data.Username
            };
        }
    }
}