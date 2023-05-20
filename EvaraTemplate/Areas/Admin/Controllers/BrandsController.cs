using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using EvaraTemplate.ViewModels.BrandVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly EvaraDbContext _evaraDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandsController(EvaraDbContext evaraDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _evaraDbContext = evaraDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<GetBrandVM> brands = new List<GetBrandVM>();
            foreach (Brand brand in _evaraDbContext.Brands.ToList())
            {
                brands.Add(new GetBrandVM()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    ImageName = brand.ImageName
                });
            }
            return View(brands);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SetBrandVM newBrand)
        {
            if (newBrand == null)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(newBrand);
            }
            newBrand.ImageName = Guid.NewGuid().ToString() + newBrand.formImage.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "banner", newBrand.ImageName);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await newBrand.formImage.CopyToAsync(fileStream);
            }
            Brand brand = new Brand()
            {
                Name = newBrand.Name,
                ImageName = newBrand.ImageName,
            };
            await _evaraDbContext.Brands.AddAsync(brand);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Brand? brand = await _evaraDbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            if (brand.ImageName != null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "banner", brand.ImageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _evaraDbContext.Brands.Remove(brand);
            _evaraDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Brand? brand = await _evaraDbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            SetBrandVM newBrand = new SetBrandVM()
            {
                Name = brand.Name,
                ImageName = brand.ImageName
            };
            return View(newBrand);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id,SetBrandVM? updatedBrand)
        {
            Brand? brand = await _evaraDbContext.Brands.AsNoTracking().Where(b=>b.Id==id).FirstOrDefaultAsync();
            if (updatedBrand == null)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedBrand);
            }
            if(updatedBrand.ImageName==null || updatedBrand.ImageName != brand.ImageName)
            {
                updatedBrand.ImageName = Guid.NewGuid().ToString() + updatedBrand.formImage.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "banner", updatedBrand.ImageName);
                using(FileStream fileStream=new FileStream(path, FileMode.Create))
                {
                    await updatedBrand.formImage.CopyToAsync(fileStream);
                }
            }
            Brand newBrand = new Brand()
            {
                Id = brand.Id,
                Name = updatedBrand.Name,
                ImageName = updatedBrand.ImageName
            };
            _evaraDbContext.Brands.Update(newBrand);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Brand? brand = await _evaraDbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            GetBrandVM brandVM = new GetBrandVM()
            {
                Name = brand.Name,
                ImageName = brand.ImageName
            };
            return View(brandVM);
        }
    }
}
