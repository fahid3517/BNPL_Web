using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class AspNetMembership
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ForceResetPassword { get; set; }
    }
}
