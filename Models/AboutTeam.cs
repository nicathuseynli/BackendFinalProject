using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class AboutTeam : BaseEntity<int>
{
    public string Description { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Profession { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

}
