using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Base.Domain.Entities.Cadastros.Base
{
    public class ApplicationUser : IdentityUser<int>
    {        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int PerfilId { get; set; }
        public bool Ativo { get; set; }        
        public async Task<IdentityResult> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateAsync(this);
            return userIdentity;
        }
    }
}
