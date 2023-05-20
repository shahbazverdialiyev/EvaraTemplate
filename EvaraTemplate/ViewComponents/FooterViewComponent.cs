using EvaraTemplate.DAL;
using EvaraTemplate.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvaraTemplate.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly EvaraDbContext _evaraDbContext;

        public FooterViewComponent(EvaraDbContext evaraDbContext)
        {
            _evaraDbContext = evaraDbContext;
        }
        public IViewComponentResult Invoke()
        {
            Dictionary<string,Setting> settings = _evaraDbContext.Settings.ToDictionary(s=>s.Key);
            return View(settings);
        }
    }
}
