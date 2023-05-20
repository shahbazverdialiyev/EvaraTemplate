using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using EvaraTemplate.ViewModels.ProductVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            List < Product >= _evaraDbContext.Products.ToList();
        }
        public IActionResult Create()
        {
            ViewData["catagories"] = _evaraDbContext.Catagories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SetProductVM newproduct)
        {
            if (newproduct == null)
            {
                ViewData["catagories"] = _evaraDbContext.Catagories.ToList();
                return View();
            }
            if (!ModelState.IsValid)
            {
                ViewData["catagories"] = _evaraDbContext.Catagories.ToList();
                return View(newproduct);
            }
            Product product = new Product()
            {
                Name = newproduct.Name,
                Price = newproduct.Price,
                Rate = newproduct.Rate,
                CatagoryId = newproduct.CatagoryId
            };
            foreach (FormFile formImage in newproduct.formImages)
            {
                string newFileName = Guid.NewGuid().ToString() + formImage;
                string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", newFileName);
                using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
                {
                    await formImage.CopyToAsync(fileStream);
                }
                product.Images.Add(new Image()
                {
                    Name = newFileName,
                    IsMain = false
                });
            }
            product.Images.FirstOrDefault().IsMain = true;
            product.Tags = newproduct.Tags;

            await _evaraDbContext.Products.AddAsync(product);
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
        public async Task<IActionResult> Update(int id, Product updatedProduct)
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
