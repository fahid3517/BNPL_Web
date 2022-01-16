using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class CusTransactionsLogs
    {
        [Key]
        public Guid Id { get; set; }
        public string? CustomerId { get; set; }
        public string? CardNumber { get; set; }
        public string? TransactionId { get; set; }
        public string? PaymentId { get; set; }
        public long? Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Type { get; set; }
    }
}
