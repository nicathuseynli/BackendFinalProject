using Microsoft.AspNetCore.Identity;

namespace Backend_Final_Project.Models;
public class AppUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
