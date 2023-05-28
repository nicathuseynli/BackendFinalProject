using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateTestimonialVM
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

    [Required]
    public string Profession { get; set; }

    [Required]
    public string Comment { get; set; }
}
