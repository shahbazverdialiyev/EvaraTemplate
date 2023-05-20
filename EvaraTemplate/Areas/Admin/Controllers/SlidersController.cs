using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly EvaraDbContext _evaraDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(EvaraDbContext evaraDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _evaraDbContext = evaraDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            return View(_evaraDbContext.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            string newFileName = Guid.NewGuid().ToString() + slider.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "slider", newFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await slider.Image.CopyToAsync(fileStream);
            }
            slider.ImageName = newFileName;
            await _evaraDbContext.Sliders.AddAsync(slider);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            if (slider.ImageName != null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "slider", slider.ImageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _evaraDbContext.Sliders.Remove(slider);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Slider? updatedSlider)
        {
            Slider? slider = await _evaraDbContext.Sliders
                .AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();
            if (updatedSlider == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (updatedSlider.ImageName != slider.ImageName || updatedSlider.ImageName == null)
            {
                updatedSlider.ImageName = Guid.NewGuid().ToString() + updatedSlider.Image.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "slider", slider.ImageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "slider", updatedSlider.ImageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await updatedSlider.Image.CopyToAsync(fileStream);
                }
            }
            updatedSlider.Id = slider.Id;
            _evaraDbContext.Sliders.Update(updatedSlider);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
