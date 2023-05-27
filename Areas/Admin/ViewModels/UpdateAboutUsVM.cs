using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;

public class UpdateAboutUsVM
{
    public int Id { get; set; }

    public string Information { get; set; }

    public string VideoLink { get; set; }

    public string CenterTitle { get; set; }

    public string Image { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

}
