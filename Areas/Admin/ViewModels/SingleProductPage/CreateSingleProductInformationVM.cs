using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateSingleProductInformationVM
{
    [Required]
    public string ShippingInfo { get; set; }

    [Required]
    public string AboutReturnInfo { get; set; }

    [Required]
    public string GuaranteeInfo { get; set; }
}
