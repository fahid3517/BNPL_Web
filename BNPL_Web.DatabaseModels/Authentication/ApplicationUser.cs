using System;
using System.ComponentModel;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Identity;
namespace BNPL_Web.DatabaseModels.DbImplementation
{
    public class ApplicationUser : IdentityUser
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDisable { get; set; }
        public DateTime? FirstLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? SuccessFullLogin { get; set; }
        public DateTime? LastLogout  { get; set; }
    }
}
