using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateContactInfoVM
{

    [Required]
    public string ContactDescription { get; set; }

    [Required]
    public string MailAddress { get; set; }

    [Required]
    public int PhoneNumber { get; set; }

    [Required]
    public string Address { get; set; }
}
