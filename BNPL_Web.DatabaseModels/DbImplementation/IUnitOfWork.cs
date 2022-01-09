
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> AspNetUser { get; }
        IRepository<AspNetRoles> AspNetRoles { get; }
        IRepository<AspNetProfile> AspNetProfile { get; }
        IRepository<AspNetProfileRoles> AspNetProfileRoles { get; }
        IRepository<AspNetMembership> AspNetMembership { get; }
        IRepository<CustomerProfile> CustomerProfile { get; }
        IRepository<UserProfiles> UserProfiles { get; }
        IRepository<SystemUsers> SystemUsers { get; }
    }
}
