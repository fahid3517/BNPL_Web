
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> AspNetUser { get; }

        IRepository<ApplicationRole> AspNetRole { get; }
        IRepository<ApplicationUserRole> AspNetUserRole { get; }
        
        IRepository<AspNetRoles> Privilages { get; }
        IRepository<RolePrivilages> RolePrivilages { get; }
        IRepository<CustomerProfile> CustomerProfile { get; }
        IRepository<BackOfficeUserProfile> BackOfficeUserProfile { get; }
        IRepository<SystemUsersProfile> SystemUsersProfile { get; }

    }
}
