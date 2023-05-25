using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateSliderVM
{
    public int Id { get; set; }

    [Required]
    public string Label { get; set; }

    [Required]
    public string Description { get; set; }

    public string Percent { get; set; }

    public IFormFile Photo { get; set; }

    [AllowNull]
    public string Image { get; set; }

}
