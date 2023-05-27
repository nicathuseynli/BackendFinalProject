using Backend_Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<HomeProduct> HomeProducts { get; set; }
        public DbSet<HomeCategory> HomeCategories { get; set; }
        public DbSet<About> AboutPages { get; set; }
        public DbSet<AboutTeam> AboutTeamMembers{ get; set; }
        public DbSet<AboutCompanySlider> AboutCompanySliders { get; set; }
        public DbSet<ContactInfo> ContactInformations { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}

