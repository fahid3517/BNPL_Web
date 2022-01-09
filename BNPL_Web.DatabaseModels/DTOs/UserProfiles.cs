using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class UserProfiles
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}