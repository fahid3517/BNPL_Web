using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class UserProfiles
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}
