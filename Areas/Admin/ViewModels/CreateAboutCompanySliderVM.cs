using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateAboutCompanySliderVM
{
    [Required]
    public IFormFile Photo { get; set; }
}
