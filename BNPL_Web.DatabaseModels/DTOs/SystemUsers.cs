using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class SystemUsers
    {
        [Key]
        public Guid SystemUserId { get; set; }
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
    }
}
