using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    [Table("t_BackOfficeUsers")]
    public class TBackOfficeUser
    {
        [Key]
        [StringLength(500)]
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaxRolesAllowed { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLogin { get; set; }

    }
}
