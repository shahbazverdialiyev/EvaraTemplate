using System.ComponentModel.DataAnnotations;

namespace EvaraTemplate.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "boş qala bilmez")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "boş qala bilmez")]
        public bool IsMain { get; set; }
        [Required(ErrorMessage = "boş qala bilmez")]
        public ICollection<Product> Products { get; set; } = null!;
    }
}
