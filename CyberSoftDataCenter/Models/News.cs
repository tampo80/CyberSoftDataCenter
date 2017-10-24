using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class News
    {
        public int NewsId { get; set; }

        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        public string Auteur { get; set; }

        [Display(Name = "Date")]
        public DateTime DateNews { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
