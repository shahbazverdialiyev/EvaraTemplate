using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}