

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
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<ApplicationRole> AspNetRole { get; set; }

        public IRepository<ApplicationUser> AspNetUser { get; set; }

        public IRepository<Privilages> Privilages { get; set; }

        public IRepository<RolePrivilages> RolePrivilages { get; set; }

        public UnitOfWork(IRepository<ApplicationRole> AspNetRole, IRepository<ApplicationUser> AspNetUser
            , IRepository<Privilages> Privilages, IRepository<RolePrivilages> RolePrivilages)
        {
            this.AspNetRole = AspNetRole;
            this.AspNetUser= AspNetUser;
            this.Privilages = Privilages;
            this.RolePrivilages= RolePrivilages;
        }
    }
}
