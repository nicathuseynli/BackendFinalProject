using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class HomeProduct:BaseEntity<int>
{

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public decimal Price { get; set; }

    [MaxLength(10)]
    public decimal Rating { get; set; }

    public string Image { get; set; }

    public string HoverImage { get; set; }

    public string Description { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }   

    [NotMapped]
    public IFormFile HoverPhoto { get; set; }

    //navigation
    public virtual HomeCategory HomeCategory { get; set; }

    public virtual ShopPageColour ShopPageColour { get; set; }

    public int HomeCategoryId { get; set; }

    public int ShopPageColourId { get; set; }

}
