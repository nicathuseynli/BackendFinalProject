using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateSingleProductDescriptionVM
{
    [Required]
    public string Information { get; set; }
}
