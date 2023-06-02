using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateShopPageColourVM
{
    [Required]
    public string Colour { get; set; }
}
