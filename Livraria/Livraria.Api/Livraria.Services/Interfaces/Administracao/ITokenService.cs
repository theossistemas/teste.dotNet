using Livraria.Domain.Entities.Administracao;

namespace Livraria.Services.Interfaces.Administracao
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
