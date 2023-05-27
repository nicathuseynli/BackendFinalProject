
using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateBannerVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    public string BannerImage { get; set; }
}
