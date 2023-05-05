using EvaraTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaraTemplate.DAL
{
    public class EvaraDbContext:DbContext
    {
        public EvaraDbContext(DbContextOptions<EvaraDbContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
