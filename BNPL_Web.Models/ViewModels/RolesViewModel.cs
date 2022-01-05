using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BNPL_Web.Common.ViewModels
{
    public class RolesViewModel
    {
        [Display(Name = "Role")]
        public String Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [RegularExpression(@"^[a-zA-Z ]{0,50}$", ErrorMessage = "The User Role Name can only be alphanumeric and 50 characters long.")]
        public string RoleName { get; set; }

        public List<SelectListItem> allPrivelages { get; set; }

        public Boolean hasChildren { get; set; }

        public string selected { get; set; }

        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,}$", ErrorMessage = "Password must contain: Minimum 10 characters, atleast 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character.")]
        public string password { get; set; }

        public List<string> roleOfuser { get; set; }//stores role id

        public string Name { get; set; }
    }
}
