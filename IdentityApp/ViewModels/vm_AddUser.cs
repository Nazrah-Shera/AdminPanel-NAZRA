using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class vm_AddUser
    {
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "The full name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits.")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please select the no. of roles you want to allow the user.")]
        public int MaxRolesAllowed { get; set; }

        public bool IsActive { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastLogin { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPaaword { get; set; }
    }
}
