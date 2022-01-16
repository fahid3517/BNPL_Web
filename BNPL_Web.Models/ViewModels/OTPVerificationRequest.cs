using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class OTPVerificationRequest
    {
        public string? CivilID { get; set; }
        public string? Number { get; set; }
        public string? OTP { get; set; }
    }
}
