using System.ComponentModel.DataAnnotations;

namespace EvaraTemplate.ViewModels.BrandVM
{
    public class SetBrandVM
    {
        [Required(ErrorMessage ="bosh qala bilmez!!")]
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public IFormFile formImage { get; set; }
    }
}
