using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraTemplate.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "boş qala bilmez"), MaxLength(30, ErrorMessage = "maksimum uzunluq 30 ola biler")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "boş qala bilmez")]
        public decimal? Price { get; set; } = null!;
        public int? Rate { get; set; }
        [Required(ErrorMessage = "boş qala bilmez")]
        public ICollection<Image> Images { get; set; } = null!;
        public int? CatagoryId { get; set; }
        public Catagory? Catagory { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        [NotMapped]
        public IFormFileCollection formImages { get; set; }
    }
}
