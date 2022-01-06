using System;
using System.ComponentModel;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Identity;
namespace BNPL_Web.DatabaseModels.DbImplementation
{
    //[TypeConverter(typeof(Converter))]
    public class ApplicationUser : IdentityUser
    {
        public virtual ApplicationUserRole AspNetUserRoles { get; set; }
    }
}
