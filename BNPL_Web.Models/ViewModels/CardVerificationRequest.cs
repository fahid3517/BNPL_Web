using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class CardVerificationRequest
    {
        public string? CivilId { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? Token { get; set; }

    }
}
