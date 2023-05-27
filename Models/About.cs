using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class About : BaseEntity<int>
{
    public string Information { get; set; }

    public string VideoLink { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    public string CenterTitle { get; set; }

    public string Image { get; set; }

}
