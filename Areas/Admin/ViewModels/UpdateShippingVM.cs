﻿using System.Diagnostics.CodeAnalysis;

namespace Backend_Final_Project.Areas.Admin.ViewModels
{
    public class UpdateShippingVM
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        [AllowNull]
        public IFormFile Photo { get; set; }

        public string Image { get; set; }
    }
}
