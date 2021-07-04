using System.Runtime.Serialization;

namespace Livraria.Domain.Entities.Administracao
{
    public enum TipoUsuario
    {
        [EnumMember(Value = "USUÁRIO")]
        Usuario,
        [EnumMember(Value = "ADMIN")]
        Admin
    }
}
