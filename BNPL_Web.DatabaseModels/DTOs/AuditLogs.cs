using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class AuditLogs
    {
        [Key]
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? EventType { get; set; }
        public string? TableName { get; set; }
        public string? Columns { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }
    }
}
