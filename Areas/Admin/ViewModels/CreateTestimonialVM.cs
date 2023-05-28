using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateTestimonialVM
{

    public string Information { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

    [Required]
    public string Profession { get; set; }

    [Required]
    public string Comment { get; set; }
}
