using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class Privilages
    {
        public int Id { get; set; }
        public string Privilege { get; set; }
        public string Category { get; set; }
        public string Portal { get; set; }
        public int? SortOrder { get; set; }
        public virtual ICollection<RolePrivilages> DbRolePrivileges { get; set; }
    }
}
