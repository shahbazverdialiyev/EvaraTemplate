using Microsoft.AspNetCore.Mvc;

namespace EvaraTemplate.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult Register() { return View(); }
    }
}
