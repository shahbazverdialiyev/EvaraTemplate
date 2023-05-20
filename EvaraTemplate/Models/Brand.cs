using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraTemplate.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile formImage { get; set; }
    }
}
