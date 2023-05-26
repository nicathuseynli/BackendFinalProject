using Backend_Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ShopProduct> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
    }
}

