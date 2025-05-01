using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    [Table("t_ApplicationLogs")]

    public class TApplicationLogs
    {
        [Key]
        public Guid ID { get; set; }
        public string UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LogLevel { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string IPAddress { get; set; }
        public string RequestParameters { get; set; }
        public string ResponseParameters { get; set; }

    }
}
