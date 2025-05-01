using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    [Table("t_ExternalLogs")]
    public class TExternalLogs
    {
        [Key]
        public Guid ID { get; set; }
        public string ApplicationLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ServiceName { get; set; }
        public string Endpoint { get; set; }
        public string RequestPayload { get; set; }
        public string ResponsePayload { get; set; }
        public int StatusCode { get; set; }
        public int DurationMs { get; set; }
        public bool IsSuccess { get; set; }
        public string Exception { get; set; }
        public string CalledByUserId { get; set; }
    }
}
