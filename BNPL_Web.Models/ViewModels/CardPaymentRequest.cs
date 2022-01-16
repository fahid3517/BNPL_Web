using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class CardPaymentRequest
    {
        public RequestSource source { get; set; }
        public string currency { get; set; }
        public long amount { get; set; }
        public Metadata metadata { get; set; }
    }
    public class RequestSource
    {
        public string type { get; set; }
        public string token { get; set; }
    }
    public class Metadata
    {
        public string udf1 { get; set; }
        public string coupon_code { get; set; }
        public string partner_id { get; set; }
    }
}
