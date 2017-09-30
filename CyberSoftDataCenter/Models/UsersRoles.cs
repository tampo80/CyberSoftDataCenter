using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class UsersRoles
    {
        public int UsersRolesID { get; set; }
        public int UsersID { get; set; }
        public int RolesID { get; set; }

        //navigation
        public virtual Users Users { get; set; }
        public virtual Roles Roles { get; set; }

    }
}
