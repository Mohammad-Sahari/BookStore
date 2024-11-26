﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignInModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
