using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Output;
using System.Threading.Tasks;

namespace LivrariaJc.Domain.Interfaces.Services
{
    public interface IAutenticarServices
    {
        ServiceResult Login(LoginInput input);
    }
}
