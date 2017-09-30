using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Roles
    {
        public Roles()
        {
            this.RolesAttributs = new HashSet<RolesAttributs>();
            this.UsersRoles = new HashSet<UsersRoles>();
        }
        public int RolesID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        //navigation
        public virtual ICollection<RolesAttributs> RolesAttributs { get; set; }
        public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
