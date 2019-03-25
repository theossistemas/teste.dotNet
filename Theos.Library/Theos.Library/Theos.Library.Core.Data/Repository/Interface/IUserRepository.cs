using Theos.Library.Core.Data.Repository.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.Domain.Users;

namespace Theos.Library.Core.Data.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User, BaseFilter>
    {
        User Login(string login, string password);
    }
}
