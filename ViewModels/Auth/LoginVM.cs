﻿using System.ComponentModel.DataAnnotations;

namespace Backend_Final_Project.ViewModels.Account;
public class LoginVM
{
    [Required, DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
}
