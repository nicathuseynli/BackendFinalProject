using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class UpdateSingleProductVM
{
    public int Id { get; set; }

    public string Information { get; set; }

    public string ShippingInfo { get; set; }

    public string AboutReturnInfo { get; set; }

    public string GuaranteeInfo { get; set; }

    public int CommentCount { get; set; }

    public string UserFullname { get; set; }

    public string UserComment { get; set; }
    [AllowNull]
    public IFormFile UserPhoto { get; set; }

    public string UserImage { get; set; }
}
