using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class CybersCenters
    {
        public CybersCenters()
        {
            this.VenteUnites = new HashSet<VenteUnites>();
        }
        public int CybersCentersID { get; set; }
        public string Nom { get; set; }
        public string Tel { get; set; }
        public int VillesID { get; set; }

        //navigation
        public virtual   Villes Villes { get; set; }
        public virtual ICollection<VenteUnites> VenteUnites { get; set; }
    }
}
