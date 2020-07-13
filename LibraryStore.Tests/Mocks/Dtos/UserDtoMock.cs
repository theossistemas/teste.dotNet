using LibraryStore.Core.Data.Dtos;
using LibraryStore.Tests.Extensions;
using data = LibraryStore.Tests.Mocks.Datas.UserData;

namespace LibraryStore.Tests.Mocks.Dtos
{
    public static class UserDtoMock
    {
        public static UserDto CreateAdmin()
        {
            return new UserDto
            {
                Id = data.Id.ToGuid(),
                Fullname = data.Fullname,
                Username = data.Username,
                CreatedAt = data.CreatedAt.ToDateTime(),
                Active = data.Active
            };
        }
    }
}