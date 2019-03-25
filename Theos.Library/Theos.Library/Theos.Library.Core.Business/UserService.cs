using Theos.Library.Core.Business.Base;
using Theos.Library.Core.Business.Interface;
using Theos.Library.Core.Data.Repository.Interface;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.Domain.Users;

namespace Theos.Library.Core.Business
{
    public class UserService : BaseService<User, BaseFilter>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public User Login(string login, string password)
        {
            return ((IUserRepository) Repository).Login(login, password);
        }
    }
}
