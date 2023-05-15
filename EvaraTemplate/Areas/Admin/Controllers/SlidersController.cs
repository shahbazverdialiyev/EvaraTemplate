using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider==null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Update(int id, Slider updatedSlider)
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
            updatedSlider.Id = slider.Id;
            slider.Offer = updatedSlider.Offer;
            //string path=Path.Combine(_environment.WebRootPath,)   
            _evaraDbContext.Sliders.Update(updatedSlider);
            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
