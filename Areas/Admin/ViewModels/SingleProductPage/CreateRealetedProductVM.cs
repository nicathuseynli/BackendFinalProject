using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateRealetedProductVM
{
    [Required]
    public string Information { get; set; }
}
