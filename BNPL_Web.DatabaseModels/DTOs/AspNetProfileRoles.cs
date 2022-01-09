using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class AspNetProfileRoles
    {
        public Guid RoleId { get; set; }
        public Guid ProfileId { get; set; }
        public virtual AspNetRoles Roles { get; set; }
        public virtual AspNetProfile Profile { get; set; }
    }
}
