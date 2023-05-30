using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.ViewModels.Account;
public class LoginVM
{
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    public string UserName { get; set; }
}
