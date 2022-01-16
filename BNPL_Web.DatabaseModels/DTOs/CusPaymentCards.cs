using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class CusPaymentCards
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CustomerId { get; set; }
        public string? CardNumber { get; set; }
        public string? Token { get; set; }
        public bool? IsDefault { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
