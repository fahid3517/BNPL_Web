using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class RolePrivilages
    {
        public int Id { get; set; }
        public int PrivilegeId { get; set; }
        public string RoleId { get; set; }
        public virtual Privilages Privilege { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
