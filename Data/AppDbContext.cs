using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<HomeProduct> HomeProducts { get; set; }
        public DbSet<About> AboutPages { get; set; }
        public DbSet<AboutTeam> AboutTeamMembers{ get; set; }
        public DbSet<AboutCompanySlider> AboutCompanySliders { get; set; }
        public DbSet<ContactInfo> ContactInformations { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<NewProduct> NewProducts { get; set; }
        public DbSet<HomeDescription> HomeDescriptions { get; set; }
        public DbSet<QuickLink> QuickLinks { get; set; }
        public DbSet<SocialMediaAdress> SocialMediaAdresses { get; set; }
        public DbSet<HeaderPhoneNumber> HeaderPhoneNumbers { get; set; }
        public DbSet<HeaderInfo> HeaderInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

