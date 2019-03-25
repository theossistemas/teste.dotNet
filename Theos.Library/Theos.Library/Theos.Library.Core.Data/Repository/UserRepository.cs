using System.Linq;
using Theos.Library.Core.Data.Repository.Base;
using Theos.Library.Core.Data.Repository.Interface;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Helper;
using Theos.Library.Domain.Users;

namespace Theos.Library.Core.Data.Repository
{
    public class UserRepository : BaseRepository<User, BaseFilter, UserKey>, IUserRepository
    {
        public User Login(string login, string password)
        {
            using (var context = GetContext())
            {
                login = login.ToUpper();
                password = EncryptHelper.EncryptPassword(login, password);

                var response = context.Set<User>().FirstOrDefault(w => w.Active && w.Login.ToUpper().Equals(login) && w.Password.Equals(password));
                return ExtractFromContext(response);
            }
        }
    }
}
