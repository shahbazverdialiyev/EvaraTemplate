using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EvaraTemplate.Controllers
{
    public class ShopCartController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
