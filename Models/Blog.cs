using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class Blog : BaseEntity<int>
{
    public string ByPerson { get; set; }

    public string BlogTitle { get; set; }

    public string BlogInformation { get; set; }

    public string BlogImage { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }
}
