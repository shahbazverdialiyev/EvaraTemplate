using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        readonly EvaraDbContext _evaraDbContext;
        readonly IWebHostEnvironment _environment; 
        public ProductsController(EvaraDbContext evaraDbContext, IWebHostEnvironment environment)
        {
            _evaraDbContext = evaraDbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_evaraDbContext.Products
                //.Include(p=>p.Catagory)
                .ToList());
        }
        public IActionResult Create()
        {
          // ViewData["catagories"] = _evaraDbContext.Catagories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product newproduct)
        {
            if (newproduct==null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            //string path=Path.Combine(_environment.WebRootPath,)   
            await _evaraDbContext.Products.AddAsync(newproduct);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _evaraDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _evaraDbContext.Products.Remove(product);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Product? product = await _evaraDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> Update(int id)
        {
            Product? product = await _evaraDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Product updatedProduct)
        {
            Product? product = await _evaraDbContext.Products
                .AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
            if (updatedProduct == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            updatedProduct.Id = product.Id;   
            product.Name = updatedProduct.Name;
            //string path=Path.Combine(_environment.WebRootPath,)   
            _evaraDbContext.Products.Update(updatedProduct);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
