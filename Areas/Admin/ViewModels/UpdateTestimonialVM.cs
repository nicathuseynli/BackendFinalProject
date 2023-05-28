using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateTestimonialVM
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public IFormFile Photo { get; set; }

    [AllowNull]
    public string ClientImage { get; set; }

    public string Profession { get; set; }

    public string Comment { get; set; }
}
