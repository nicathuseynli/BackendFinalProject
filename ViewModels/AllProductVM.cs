using Backend_Final_Project.Models;

namespace Backend_Final_Project.ViewModels;
public class AllProductVM
{
    public HomeProduct Homeproduct { get; set; }

    public List<HomeProduct> HomeProducts { get; set; }

    public RealetedProduct RealetedProducts { get; set; }

    public List<SingleProduct> SingleProducts { get; set; }

    public SingleProduct SingleProduct { get; set; }
}
