using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class AboutCompanySlider : BaseEntity<int>
{
    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }
}
