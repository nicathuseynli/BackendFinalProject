using Backend_Final_Project.Models;

namespace Backend_Final_Project.ViewModels;
public class HomeVM
{
    public List<Slider> Sliders { get; set; }

    public List<Shipping> Shippings { get; set; }

    public List<HomeProduct> HomeProducts { get; set; }

    public List<Blog> Blogs { get; set; }

    public Blog Blog { get; set; }

    public List<AboutCompanySlider> AboutCompanySliders { get; set; }

    public List<Testimonial> Testimonials { get; set; }

    public List<NewProduct> NewProducts { get; set; }

    public HomeDescription HomeDescriptions { get; set; }

    public List<Banner> Baners { get; set; }
 
}
