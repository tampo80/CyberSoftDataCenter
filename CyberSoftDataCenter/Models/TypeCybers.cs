using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class TypeCybers
    {
        public TypeCybers()
        {
            this.CybersCenters = new HashSet<CybersCenters>();
            this.Tarifs = new HashSet<Tarifs>();
        }
        public int TypeCybersId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }


        //navigation
        public virtual ICollection<CybersCenters> CybersCenters { get; set; }
        public virtual ICollection<Tarifs> Tarifs { get; set; }
    }
}
