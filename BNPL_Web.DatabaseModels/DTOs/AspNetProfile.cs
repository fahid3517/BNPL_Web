using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class AspNetProfile
    {
        public Guid Id { get; set; }
        public string? ProfileName { get; set; }
        public string? Description { get; set; }
    }
}
