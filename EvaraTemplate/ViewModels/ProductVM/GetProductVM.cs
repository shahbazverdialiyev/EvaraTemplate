using EvaraTemplate.Models;

namespace EvaraTemplate.ViewModels.ProductVM
{
    public class GetProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Rate { get; set; }
        public ICollection<Image> Images { get; set; }
        public int? CatagoryId { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
