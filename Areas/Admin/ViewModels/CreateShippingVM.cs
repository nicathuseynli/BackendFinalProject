﻿using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.Areas.Admin.ViewModels
{
    public class CreateShippingVM
    {
            public string Label { get; set; }
            public string Description { get; set; }
            [Required]
            public IFormFile Photo { get; set; }
             
    }
}
