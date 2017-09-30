using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string FulleName { get; set; }
        [Required]
        public string Tel { get; set; }
    }
}
