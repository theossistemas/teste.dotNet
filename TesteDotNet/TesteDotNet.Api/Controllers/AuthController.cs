using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TesteDotNet.Api.ViewModel;

namespace TesteDotNet.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registeruser)
        {
            if (!ModelState.IsValid) return NotFound();
            var user = new IdentityUser
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, registeruser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(registeruser);
            }

            return NotFound();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
            if (result.Succeeded)
            {
                return Ok(loginUser);
            }

            return BadRequest("Usuário ou Senha Incorreto");
        }
    }
}
