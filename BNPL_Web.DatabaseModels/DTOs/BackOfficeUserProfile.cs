using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class BackOfficeUserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public virtual ApplicationUser UserId { get; set; }
        public string FullName { get; set; }
    }
}
