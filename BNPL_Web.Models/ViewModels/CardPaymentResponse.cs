using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class CardPaymentResponse
    {
        public string id { get; set; }
        public string action_id { get; set; }
        public long amount { get; set; }
        public string currency { get; set; }
        public bool approved { get; set; }
        public string status { get; set; }
        public string auth_code { get; set; }
        public string eci { get; set; }
        public string scheme_id { get; set; }
        public string response_code { get; set; }
        public string response_summary { get; set; }
        public Risk risk { get; set; }
        public Source source { get; set; }
        public Customer customer { get; set; }
        public string processed_on { get; set; }
        public string reference { get; set; }
    }
  
    public class Risk
    {
        public bool flagged { get; set; }
    }
    public class Source
    {
        public string id { get; set; }
        public string type { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string scheme { get; set; }
        public string last4 { get; set; }

        public string fingerprint { get; set; }
        public string bin { get; set; }
        public string card_type { get; set; }
        public string card_category { get; set; }
        public string issuer { get; set; }
        public string issuer_country { get; set; }
        public string product_id { get; set; }
        public string product_type { get; set; }
        public string avs_check { get; set; }
        public string cvv_check { get; set; }
    }
    public class Customer
    {
        public string id { get; set; }
    }
    public class Links
    {
        public Self self { get; set; }
        public Actions actions { get; set; }
        public Capture capture { get; set; }
        //public void1 void {get;set;}
    }
    public class Self
    {
        public string href { get; set; }
    }
    public class Actions
    {
        public string href { get; set; }
    }
    public class Capture
    {
        public string href { get; set; }
    }
    public class void1
    {
        public string href { get; set; }
    }
}
