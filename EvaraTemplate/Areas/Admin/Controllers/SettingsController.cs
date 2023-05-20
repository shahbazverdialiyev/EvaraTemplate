using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly EvaraDbContext _evaraDbContext;

        public SettingsController(EvaraDbContext evaraDbContext)
        {
            _evaraDbContext = evaraDbContext;
        }

        public async Task<IActionResult> Index()
        {
            Dictionary<string, Setting> settings = await _evaraDbContext.Settings.ToDictionaryAsync(s => s.Key);
            return View(settings);
        }
        public async Task<IActionResult> Update()
        {
            Dictionary<string, Setting> settings = await _evaraDbContext.Settings.ToDictionaryAsync(s => s.Key);
            return View(settings);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Dictionary<string,Setting> newSettings)
        {
            //Dictionary<string, Setting> settings = await _evaraDbContext.Settings.ToDictionaryAsync(s => s.Key);
            //foreach(string key in newSettings.Keys)

            //if (settings[key].Value != newSettings[key].Value)

            foreach (Setting item in newSettings.Values)
            {
                _evaraDbContext.Settings.Update(item); 
            }

            await _evaraDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
