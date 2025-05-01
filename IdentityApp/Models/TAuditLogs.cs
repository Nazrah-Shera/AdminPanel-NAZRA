using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    [Table("t_AuditLogs")]
    public class TAuditLogs
    {

        [Key]
        public Guid ID { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Reason { get; set; }
        public string Module { get; set; }

        //Indicates if it's a manual change (done by a user) or an automatic change (done by a scheduled job, system event, etc.).
        public string ChangeType { get; set; }


    }
}
