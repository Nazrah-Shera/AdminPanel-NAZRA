using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    [Table("t_ApplicationUsers")]
    public class TApplicationUser
    {
        [Key]
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaxRolesAllowed { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Password { get; set; }
        public string ConfirmPaaword { get; set; }
    }
}
