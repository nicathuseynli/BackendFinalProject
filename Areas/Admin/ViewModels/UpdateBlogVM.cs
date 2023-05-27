using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class UpdateBlogVM
{
    public int Id { get; set; }

    public string BlogDescription { get; set; }

    public string ByPerson { get; set; }

    public string BlogTitle { get; set; }

    public string BlogInformation { get; set; }

    public string BlogImage { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }
}
