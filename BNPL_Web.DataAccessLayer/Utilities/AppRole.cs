using BNPL_Web.DatabaseModels.DbImplementation;
using Microsoft.AspNetCore.Identity;

namespace BNPL_Web.Authentications
{
    public class AppRole : IdentityRole
    {
        public virtual ICollection<ApplicationUser> DbRolePrivileges { get; set; }
    }
}
