using Theos.Library.Core.Business.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.Domain.Users;

namespace Theos.Library.Core.Business.Interface
{
    public interface IUserService : IBaseService<User, BaseFilter>
    {
        User Login(string login, string password);
    }
}
