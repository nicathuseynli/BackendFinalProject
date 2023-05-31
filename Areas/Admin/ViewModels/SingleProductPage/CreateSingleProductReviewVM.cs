using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
public class CreateSingleProductReviewVM
{
    [Required]
    public int CommentCount { get; set; }

    [Required]
    public string UserFullname { get; set; }

    [Required]
    public string UserComment { get; set; }

    [AllowNull]
    public IFormFile UserPhoto { get; set; }
}
