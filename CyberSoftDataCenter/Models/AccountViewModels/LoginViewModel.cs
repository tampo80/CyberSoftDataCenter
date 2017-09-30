using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
      //  [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }

    }
}
