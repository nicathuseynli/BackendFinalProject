using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateSingleProductVM
{
    [Required]
    public string Information { get; set; }

    [Required]
    public string ShippingInfo { get; set; }

    [Required]
    public string AboutReturnInfo { get; set; }

    [Required]
    public string GuaranteeInfo { get; set; }

    [Required]
    public int CommentCount { get; set; }

    [Required]
    public string UserFullname { get; set; }

    [Required]
    public string UserComment { get; set; }

    [AllowNull]
    public IFormFile UserPhoto { get; set; }
}
