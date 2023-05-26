using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class Shipping:BaseEntity<int>
{
    public string Label { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    [NotMapped]
    public IFormFile Photo { get; set; }
}
