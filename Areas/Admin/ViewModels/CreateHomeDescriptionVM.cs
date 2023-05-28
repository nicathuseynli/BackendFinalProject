using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateHomeDescriptionVM
{
    [Required]
    public string NewProductInfo { get; set; }

    [Required]
    public string TestimonialInfo { get; set; }

    [Required]
    public string BlogInfo { get; set; }
}
