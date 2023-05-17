using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using EvaraTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EvaraTemplate.Controllers
{
    public class HomeController : Controller
    {
        EvaraDbContext _evaraDbContext;
        public HomeController(EvaraDbContext evaraDbContext)
        {
            _evaraDbContext = evaraDbContext;
        }
        public IActionResult Index()
        {
            return View(_evaraDbContext);
        }
        public IActionResult AddCart(int id)
        {
            Product? product = _evaraDbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            List<CartVM> cartsCokkie = new List<CartVM>();
            string? value = HttpContext.Request.Cookies["basket"];
            if (value == null)
            {
                HttpContext.Response.Cookies.Append("basket", JsonSerializer.Serialize(cartsCokkie));
            }
            else
            {
                cartsCokkie = JsonSerializer.Deserialize<List<CartVM>>(HttpContext.Request.Cookies["basket"]);
            }
            CartVM? cart = cartsCokkie.Find(p => p.Id == id);
            if (cart == null)
            {
                cartsCokkie.Add(new CartVM()
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                cart.Count++;
            }
            HttpContext.Response.Cookies.Append("bakset", JsonSerializer.Serialize(cartsCokkie));
            return RedirectToAction(nameof(Index));
        }
        // GetCookie ShopCartdadi
    }
}