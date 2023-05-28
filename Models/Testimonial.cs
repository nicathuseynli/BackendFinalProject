using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class Testimonial : BaseEntity<int>
{
    public string Information { get; set; }

    public string FullName { get; set; }

    public string ClientImage { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    public string Profession { get; set; }

    public string Comment { get; set; }
}
