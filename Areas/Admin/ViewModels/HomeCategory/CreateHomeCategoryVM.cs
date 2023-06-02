using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.HomeCategory;
public class CreateHomeCategoryVM
{
    [Required]
    public string Name { get; set; }
}
