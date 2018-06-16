using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserDashboard.Models{
    public class UpdateViewModel{
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [MinLength(2, ErrorMessage="First Name must have at least 2 characters")]
        public string firstname {get;set;}
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Last Name can only contain letters")]
        [MinLength(2, ErrorMessage="Last Name must have at least 2 characters")]
        public string lastname {get;set;}
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Email entered has invalid format.")]
        public string email {get;set;}
        [Required]
        [MinLength(8, ErrorMessage= "Password must have at least 8 characters")]
        [RegularExpression(@"^.*(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[!@#$%])(?=.*\d)[a-zA-Z0-9!@#$%]+$",ErrorMessage="Password must have at least: 1 number, 1 letter, 1 UpperCase letter, and 1 special character")]
        public string password {get;set;}
        [Compare(nameof(password), ErrorMessage = "Password Confirmation does not match Password")]
        public string passwordconfirm {get;set;}
    }
}