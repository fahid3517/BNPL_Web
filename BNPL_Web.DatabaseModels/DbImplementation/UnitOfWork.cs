

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.DatabaseModels.Models;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public BNPL_Context BNPL_Context;

        public IRepository<AspNetUser> AspNetUser { get; set; }

        public IRepository<AspNetRole> AspNetRole { get; set; }

        public IRepository<AspNetProfile> AspNetProfile { get; set; }

        public IRepository<AspNetProfileRole> AspNetProfileRole { get; set; }

        public IRepository<AspNetMembership> AspNetMembership { get; set; }
        public IRepository<CustomerProfile> CustomerProfile { get; set; }
        public IRepository<UserProfile> UserProfile { get; set; }

        public IRepository<SystemUser> SystemUsers { get; set; }
        public UnitOfWork(BNPL_Context BNPL_Context, IRepository<AspNetRole> AspNetRole, IRepository<AspNetUser> AspNetUser
            , IRepository<AspNetProfile> AspNetProfile, IRepository<AspNetProfileRole> AspNetProfileRole
            , IRepository<AspNetMembership> AspNetMembership, IRepository<CustomerProfile> CustomerProfile
            , IRepository<UserProfile> UserProfile, IRepository<SystemUser> SystemUsers)
        {
            this.BNPL_Context=BNPL_Context;
            this.AspNetUser=AspNetUser;
            this.AspNetRole=AspNetRole;
            this.AspNetProfile = AspNetProfile;
            this.AspNetProfileRole = AspNetProfileRole;
            this.AspNetMembership = AspNetMembership;
            this.CustomerProfile=CustomerProfile;
            this.UserProfile=UserProfile;
            this.SystemUsers = SystemUsers;
        }

    }
}
