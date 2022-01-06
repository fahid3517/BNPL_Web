using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BNPL_Web.Authentications
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<RolePrivilages> DbRolePrivileges { get; set; }
    }
}
