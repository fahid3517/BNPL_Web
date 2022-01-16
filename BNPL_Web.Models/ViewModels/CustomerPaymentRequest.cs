using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class CustomerPaymentRequest
    {
        public string? Cardnumber { get; set; }
        public long? Amount { get; set; }
        public string? CivilId { get; set; }
        public string? Currency { get; set; }
    }
}
