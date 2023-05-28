using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateSocialMediaVM
{
    [Required]
    public string FacebookLink { get; set; }

    [Required]
    public string TwitterLink { get; set; }

    [Required]
    public string PinterestLink { get; set; }

    [Required]
    public string DribbbleLink { get; set; }
}
