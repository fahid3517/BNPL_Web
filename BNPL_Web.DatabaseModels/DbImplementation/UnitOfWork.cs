

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.DatabaseModels.Models;

namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public BNPL_Context BNPL_Context;
      
        public IRepository<ApplicationRole> AspNetRole { get; set; }

        public IRepository<ApplicationUser> AspNetUser { get; set; }

        public IRepository<Privilages> Privilages { get; set; }

        public IRepository<RolePrivilages> RolePrivilages { get; set; }

        public IRepository<ApplicationUserRole> AspNetUserRole { get; set; }

        public IRepository<CustomerProfile> CustomerProfile { get; set; }

        public IRepository<BackOfficeUserProfile> BackOfficeUserProfile { get; set; }

        public IRepository<SystemUsersProfile> SystemUsersProfile { get; set; }

        public UnitOfWork(BNPL_Context BNPL_Context, IRepository<ApplicationRole> AspNetRole, IRepository<ApplicationUser> AspNetUser
            , IRepository<Privilages> Privilages, IRepository<RolePrivilages> RolePrivilages, IRepository<ApplicationUserRole> AspNetUserRole
            , IRepository<CustomerProfile> CustomerProfile, IRepository<BackOfficeUserProfile> BackOfficeUserProfile
            , IRepository<SystemUsersProfile> SystemUsersProfile)
        {
            this.BNPL_Context = BNPL_Context;
            this.AspNetRole = AspNetRole;
            this.AspNetUser= AspNetUser;
            this.Privilages = Privilages;
            this.RolePrivilages= RolePrivilages;
            this.AspNetUserRole= AspNetUserRole;
            this.CustomerProfile= CustomerProfile;
            this.BackOfficeUserProfile= BackOfficeUserProfile;
            this.SystemUsersProfile = SystemUsersProfile;
        }
    }
}
