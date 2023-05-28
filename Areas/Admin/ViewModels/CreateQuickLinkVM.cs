using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateQuickLinkVM
{
    [Required]
    public string Description { get; set; }

    [Required]
    public int TelephoneNumber { get; set; }
}
