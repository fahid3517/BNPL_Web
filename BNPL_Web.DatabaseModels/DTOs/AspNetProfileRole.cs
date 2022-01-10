using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class AspNetProfileRole
    {

        [Key]
        public Guid Id { get; set; }
        public int ProfileId { get; set; }
        public Guid RoleId { get; set; }

        public virtual AspNetRole Profile { get; set; } = null!;
        public virtual AspNetProfile Role { get; set; } = null!;
    }
}
