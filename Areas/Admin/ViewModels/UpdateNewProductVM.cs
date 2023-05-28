using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateNewProductVM
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Rating { get; set; }

    public string ProductImg { get; set; }

    public string HoverImg { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    [AllowNull]
    public IFormFile HoverPhoto { get; set; }

}
