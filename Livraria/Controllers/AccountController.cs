using Livraria.Data;
using Livraria.Data.Utils;
using Livraria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Reflection.MethodBase;

namespace Livraria.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ErrorUtils _error;
        public AccountController(
           ApplicationDbContext context,
           SignInManager<IdentityUser> signInManager,
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ErrorUtils error)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _error = error;
        }

        #region GET Methods
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Logoff()
        {
            if (LogOut())
            {
                TempData["success"] = "LogOut realizado com sucesso.";
            }
            else TempData["error"] = "Não foi possível realizar o logout.";

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        #endregion

        #region POST Methods
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var iUserManager = await _userManager.FindByEmailAsync(model.Email);
                if (iUserManager != null)
                {
                    var iResult = await _signInManager.PasswordSignInAsync(iUserManager, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (iResult.Succeeded)
                    {
                        TempData["success"] = "Você está logado.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        errorMessage = "Login inválido. Verifique os campos e tente novamente.";
                    }
                }
                else
                {
                    errorMessage = "Login inválido. Usuário não encontrado.";
                }
            }
            else
            {
                var errorModels = ModelState.Select(x => x.Value.Errors)
                                            .Where(y => y.Count > 0)
                                            .ToList();
                if(errorModels.Count > 0)
                {
                    foreach (var item in errorModels)
                    {
                        errorMessage += item.ToString() + ", ";
                    }
                }
            }
            var function = GetType().Name.ToString() + "|Login";
            await _error.GenerateErrorLog(errorMessage, function);
            TempData["error"] = errorMessage;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var applicationRole = await _roleManager.FindByNameAsync("Admin");
                    if (applicationRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, applicationRole.Name);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["success"] = "Usuário criado com sucesso.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    errorMessage = "Erro no cadastro, verifique os campos e tente novamente";
                }
                
            }
            else
            {
                foreach (var modelError in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        errorMessage += error.ErrorMessage;
                    }
                }
            }
            var function = GetType().Name.ToString() + "|Register";
            await _error.GenerateErrorLog(errorMessage, function);
            TempData["error"] = errorMessage;
            return View(model);
        }
        #endregion

        #region Helpers

        private bool LogOut()
        {
            var logOut = _signInManager.SignOutAsync();
            TempData["success"] = "Logout com sucesso.";
            if (logOut.IsCompletedSuccessfully)
                return true;
            else return false;
        }

        #endregion
    }
}
