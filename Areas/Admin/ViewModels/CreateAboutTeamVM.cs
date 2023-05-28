using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateAboutTeamVM
{
    [Required]
    public string Description { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Profession { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
