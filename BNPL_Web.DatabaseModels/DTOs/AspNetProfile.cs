using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class AspNetProfile
    {
        public Guid ProfileId { get; set; }
        public string? ProfileName { get; set; }
        public string? Description { get; set; }
    }
}
