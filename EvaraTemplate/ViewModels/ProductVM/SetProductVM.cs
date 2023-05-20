using EvaraTemplate.Models;
using System.ComponentModel.DataAnnotations;

namespace EvaraTemplate.ViewModels.ProductVM
{
    public class SetProductVM
    {
        [Required(ErrorMessage = "boş qala bilmez"), MaxLength(30, ErrorMessage = "maksimum uzunluq 30 ola biler")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "boş qala bilmez")]
        public decimal? Price { get; set; } = null!;
        public int? Rate { get; set; }
        [Required(ErrorMessage = "boş qala bilmez")]
        public ICollection<Image> Images { get; set; } = null!;
        public int? CatagoryId { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public IFormFileCollection formImages { get; set; }
    }
}
