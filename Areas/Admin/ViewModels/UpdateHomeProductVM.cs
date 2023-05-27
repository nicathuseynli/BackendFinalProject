using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateHomeProductVM
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }

    public decimal Rating { get; set; }

    public IFormFile Photo { get; set; }
    [AllowNull]
    public string Image { get; set; }
}
