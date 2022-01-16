using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class CustomerPaymentRequestViewModel
    {
        public Destination destination { get; set; }
        public long amount { get; set; }
        public string currency { get; set; }
    }
    public class Destination
    {
        public string type { get; set; }
        public string number { get; set; }
        public int expiry_month { get; set; }
        public int expiry_year { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}