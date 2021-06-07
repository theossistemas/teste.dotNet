using Domain.Model.User;

namespace Domain.Interface
{
    public interface IUserService
    {
        string Authenticate(UserModel user);
    }
}
