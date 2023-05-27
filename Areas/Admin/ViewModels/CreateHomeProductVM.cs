using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateHomeProductVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }

    public decimal Rating { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
