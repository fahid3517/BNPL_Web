
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
        IRepository<AspNetUser> AspNetUser { get; }
        IRepository<AspNetRole> AspNetRole { get; }
        IRepository<AspNetProfile> AspNetProfile { get; }
        IRepository<AspNetProfileRole> AspNetProfileRole { get; }
        IRepository<AspNetMembership> AspNetMembership { get; }
        IRepository<CustomerProfile> CustomerProfile { get; }
        IRepository<UserProfile> UserProfile { get; }
        IRepository<SystemUser> SystemUsers { get; }
        IRepository<OTPVerification> OTPVerification { get; }
    }
}
