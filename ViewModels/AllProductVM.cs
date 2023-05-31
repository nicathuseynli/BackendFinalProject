using Backend_Final_Project.Models;

namespace Backend_Final_Project.ViewModels;
public class AllProductVM
{
    public List<NewProduct> NewProducts { get; set; }
    public RealetedProduct RealetedProducts { get; set; }
    public List<SingleProductReview> SingleProductReviews { get; set; }
    public SingleProductPageInformation SingleProductPageInformation { get; set; }
    public SingleProductDescription SingleProductDescription { get; set; }
}
