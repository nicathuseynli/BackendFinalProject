using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.HeaderPart;
public class CreateHeaderInfoVM
{
    [Required]
    public string Information { get; set; }
}
