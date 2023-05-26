using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class HomeProduct:BaseEntity<int>
{
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }   
}
