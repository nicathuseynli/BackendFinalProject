using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateSliderVM
{
    public int Id { get; set; }

    public string Label { get; set; }

    public string Description { get; set; }

    public string Percent { get; set; }

    public IFormFile Photo { get; set; }

    [AllowNull]
    public string Image { get; set; }

}
