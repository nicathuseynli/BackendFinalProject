using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateAboutCompanySliderVM
{
    public int Id { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
