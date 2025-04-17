using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailAvailable", controller: "Account")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
