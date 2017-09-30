using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class RolesAttributs
    {
        public int Id { get; set; }
        public int RolesId { get; set; }
        public int AttributsID { get; set; }
        public bool Value { get; set; }


        public virtual Attributs Attributs { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
