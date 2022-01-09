

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

        public IRepository<ApplicationUser> AspNetUser { get; set; }

        public IRepository<AspNetRoles> AspNetRoles { get; set; }

        public IRepository<AspNetProfileRoles> AspNetProfileRoles { get; set; }

        public IRepository<UserProfiles> UserProfiles { get; set; }

        public IRepository<SystemUsers> SystemUsers { get; set; }

        public IRepository<AspNetProfile> AspNetProfile { get; set; }

        public IRepository<AspNetMembership> AspNetMembership { get; set; }

        public IRepository<CustomerProfile> CustomerProfile { get; set; }
        public UnitOfWork(BNPL_Context BNPL_Context, IRepository<ApplicationUser> AspNetUser
            , IRepository<AspNetProfileRoles> AspNetProfileRoles, IRepository<UserProfiles> UserProfiles
            , IRepository<SystemUsers> SystemUsers, IRepository<AspNetProfile> AspNetProfile
            , IRepository<AspNetMembership> AspNetMembership, IRepository<CustomerProfile> CustomerProfile)
        {
            this.BNPL_Context = BNPL_Context;
            this.AspNetUser=AspNetUser;
            this.AspNetProfileRoles=AspNetProfileRoles;
            this.UserProfiles=UserProfiles;
            this.SystemUsers=SystemUsers;
            this.AspNetProfile=AspNetProfile;
            this.AspNetMembership = AspNetMembership;
            this.CustomerProfile = CustomerProfile;
        }
    }
}
