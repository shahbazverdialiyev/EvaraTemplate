using EvaraTemplate.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EvaraTemplate.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Register() { return View(); }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            IdentityUser user = new IdentityUser()
            {
                UserName = register.Username,
                Email = register.Email,
            };
            IdentityResult identityResult= await _userManager.CreateAsync(user, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                    return View(register);
            }
            return Json("OK");
        }
    }
}
