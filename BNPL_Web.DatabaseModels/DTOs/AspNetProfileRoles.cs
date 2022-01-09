using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class AspNetProfileRoles
    {
        [Key]
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string ProfileId { get; set; }
    }
}
