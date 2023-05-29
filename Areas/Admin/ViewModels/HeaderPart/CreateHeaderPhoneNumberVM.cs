using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels.HeaderPart;
public class CreateHeaderPhoneNumberVM
{
    [Required]
    public int PhoneNumber { get; set; }
}
