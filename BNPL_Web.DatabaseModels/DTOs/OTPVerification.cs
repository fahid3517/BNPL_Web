using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class OTPVerification
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
