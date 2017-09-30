using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class DCUsers: IdentityUser
    {
        public string FulleName { get; set; }
        public string Tel { get; set; }
    }
}
