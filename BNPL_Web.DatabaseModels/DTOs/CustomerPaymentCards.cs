using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class CustomerPaymentCards
    {
        [Key]
        public Guid Id { get; set; }
        public string? CheckoutCustomerId { get; set; }
        public string? CivilId { get; set; }
        public string? CardNumber { get; set; }
        public string? Token { get; set; }
        public bool? IsDefault { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
