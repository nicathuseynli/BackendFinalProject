using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class HomeProduct:BaseEntity<int>
{

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public decimal Price { get; set; }

    [MaxLength(50)]
    public decimal Rating { get; set; }

    public string Image { get; set; }

    public string HoverImage { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }   

    [NotMapped]
    public IFormFile HoverPhoto { get; set; }
   
}
