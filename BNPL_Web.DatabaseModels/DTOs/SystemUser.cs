using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class SystemUser
    {
        public Guid SystemUserId { get; set; }
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
    }
}
