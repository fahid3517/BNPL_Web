using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class UserProfile
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ProfileId { get; set; } = null!;
    }
}
