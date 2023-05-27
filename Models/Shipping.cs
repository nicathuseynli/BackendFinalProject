﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class Shipping:BaseEntity<int>
{
    [MaxLength(100)]
    public string Label { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }
}
