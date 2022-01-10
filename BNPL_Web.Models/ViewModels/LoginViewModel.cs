using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class LoginViewModel
    {
      
        [Required]
        [Display(Name = "CivilId")]
        public string CivilId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
    public class AdminLoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
