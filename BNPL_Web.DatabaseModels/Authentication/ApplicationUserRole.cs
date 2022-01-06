using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.DatabaseModels.DbImplementation;
using Microsoft.AspNetCore.Identity;

namespace BNPL_Web.DatabaseModels.Authentication
{
    public class ApplicationUserRole
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
