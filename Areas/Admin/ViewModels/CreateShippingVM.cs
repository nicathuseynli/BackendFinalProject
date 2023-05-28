using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels;
public class CreateShippingVM
{
     [Required]    
     public string Label { get; set; }
     [Required]
     public string Description { get; set; }
     [Required]
     public IFormFile Photo { get; set; }          
}
