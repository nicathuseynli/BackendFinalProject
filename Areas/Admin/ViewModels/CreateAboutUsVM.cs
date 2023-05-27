using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateAboutUsVM
{
    public int Id { get; set; }
    [Required]
    public string Information { get; set; }
    [Required]
    public string VideoLink { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
    [Required]
    public string CenterTitle { get; set; }

}
