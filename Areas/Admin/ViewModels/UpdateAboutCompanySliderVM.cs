using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateAboutCompanySliderVM
{
    public int Id { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }
 
    public string Image { get; set; }
}
