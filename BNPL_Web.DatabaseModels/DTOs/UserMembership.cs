using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class UserMembership
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId  { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
