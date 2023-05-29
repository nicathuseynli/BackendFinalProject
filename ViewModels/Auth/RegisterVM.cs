﻿using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.ViewModels.Account;
public class RegisterVM
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
