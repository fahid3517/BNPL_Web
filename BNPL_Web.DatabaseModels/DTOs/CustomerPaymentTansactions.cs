using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class CustomerPaymentTansactions
    {
        [Key]
        public Guid Id { get; set; }
        public string? CheckoutCustomerId { get; set; }
        public string? CheckoutPaymentId { get; set; }
        public string? CheckoutActionId { get; set; }
        public long? Amount { get; set; }
        public string? Currency { get; set; }
        public string? CardNumber { get; set; }
        public string? CivilId { get; set; }
        public bool Approved { get; set; }
        public string? Status { get; set; }
        public string? AuthCode { get; set; }
        public string? ResponseCode { get; set; }
        public string? ResponseSummary { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}