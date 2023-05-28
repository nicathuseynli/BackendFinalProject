using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class NewProduct : BaseEntity<int>
{
    public string ProductImg { get; set; }

    public string HoverImg { get; set; }

    [NotMapped]
    public IFormFile HoverPhoto { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Rating { get; set; }
}
