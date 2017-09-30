using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models.AccountViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Login")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Nom")]
        public string FulleName { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }

        [Display(Name = "Tel")]
        public string Tel { get; set; }
    }
}
