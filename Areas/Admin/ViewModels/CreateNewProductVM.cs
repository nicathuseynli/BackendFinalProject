using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateNewProductVM
{
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

    [Required]
    public IFormFile HoverPhoto { get; set; }

}
