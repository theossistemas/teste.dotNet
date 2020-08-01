using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Usuario
{
    public interface IUsuarioService
    {      
        string GerarJwt(IList<Claim> claim, string email, List<Claim> claimUsuario);                
    }
}
