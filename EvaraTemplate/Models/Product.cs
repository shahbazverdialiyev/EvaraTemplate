namespace EvaraTemplate.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public string ImgUrl { get; set; }
        public int CatagoryId { get; set; }
        public Catagory Catagory { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
