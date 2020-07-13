using LibraryStore.Core.Data.Dtos;
using data = LibraryStore.Tests.Mocks.Datas.UserData;

namespace LibraryStore.Tests.Mocks.Dtos.Inputs
{
    public static class UserInputDtoMock
    {
        public static UserInputDto CreateAdmin()
        {
            return new UserInputDto
            {
                Fullname = data.Fullname,
                Username = data.Username
            };
        }
    }
}