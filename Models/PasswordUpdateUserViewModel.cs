using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserDashboard.Models{
    public class PasswordUpdateViewModel{
        [Required(ErrorMessage= "Password is required")]
        [MinLength(8, ErrorMessage= "Password must have at least 8 characters")]
        [RegularExpression(@"^.*(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[!@#$%])(?=.*\d)[a-zA-Z0-9!@#$%]+$",ErrorMessage="Password must have at least: 1 number, 1 letter, 1 UpperCase letter, and 1 special character")]
        public string password {get;set;}
        [Compare(nameof(password), ErrorMessage = "Password Confirmation does not match Password")]
        public string passwordconfirm {get;set;}
    }
}