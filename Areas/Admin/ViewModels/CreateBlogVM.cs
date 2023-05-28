using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateBlogVM
{
    [Required]
    public string ByPerson { get; set; }

    [Required]
    public string BlogTitle { get; set; }

    [Required]
    public string BlogInformation { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
