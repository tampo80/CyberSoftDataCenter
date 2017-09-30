using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Attributs
    {
        public Attributs()
        {
            this.RolesAttributs = new HashSet<RolesAttributs>();
        }
        public int AttributsID { get; set; }
        public string AttributName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolesAttributs> RolesAttributs { get; set; }
    }
}
