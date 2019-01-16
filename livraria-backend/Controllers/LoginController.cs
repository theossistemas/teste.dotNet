using System;
using System.Linq;
using System.Threading.Tasks;
using livraria_backend.Data;
using livraria_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livraria_backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LivrariaContext _context;
        public LoginController(LivrariaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Usuario usuario)
        {
            var usuarioResult = _context.Usuarios.Where<Usuario>(u => u.Login == usuario.Login && u.Senha == usuario.Senha).FirstOrDefault();
            if(usuarioResult == null)
            {
                return NotFound();
            }
            else
            {
                usuario.Id = usuarioResult.Id;
                return await ResetToken(usuario.Id, usuario);
            }
        }

        private async Task<IActionResult> ResetToken(int id, [FromBody]Usuario usuario)
        {
            if (usuario.Id == 0)
                return NotFound();

            var usuarioResult = _context.Usuarios.Where<Usuario>(u => u.Id == usuario.Id).FirstOrDefault();
            if (usuarioResult == null)
            {
                return NotFound();
            }
            else
            {
                usuarioResult.Token = "123456";
                _context.Entry(usuarioResult).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Ok(usuarioResult);

            }

        }
    }
}