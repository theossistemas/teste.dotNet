using Data.Repository.Wrapper;
using Domain.Interface;
using Domain.Model.User;
using Service.AuthService;

namespace Service.ApplicationService
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repository { get; }
        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repository = repositoryWrapper;
        }

        public string Authenticate(UserModel user)
        {
            var userEntity = _repository.User.GetUser(user.Username, user.Password);

            if (userEntity == null)
                return default;

            var token = TokenService.GenerateToken(user);

            return token;
        }
    }
}
