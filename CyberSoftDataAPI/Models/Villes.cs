using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Villes
    {
        
            public Villes()
            {
            this.CybersCenters = new HashSet<CybersCenters>();
            }
            public int VillesID { get; set; }
            public string Nom { get; set; }
            public int PaysID { get; set; }

            //navigation    
            public virtual Pays Pays { get; set; }
            public virtual ICollection<CybersCenters> CybersCenters { get; set; }

    }
}
