using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateAboutTeamVM
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Profession { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    public string Image { get; set; }

}
