using EvaraTemplate.Models;
using EvaraTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace EvaraTemplate.Controllers
{
    public class ShopCartController:Controller
    {
        public IActionResult Index()
        {
            return View(JsonSerializer.Deserialize<List<CartVM>>(HttpContext.Request.Cookies["basket"]));
        }
    }
}
