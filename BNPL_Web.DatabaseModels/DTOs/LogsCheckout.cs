using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class LogsCheckout
    {
        public Guid Id { get; set; }
        public string? DataObj { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMsg { get; set; }
        public string? ExceptionObj { get; set; }
        public string? Type { get; set; }
        public string? CivilId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
