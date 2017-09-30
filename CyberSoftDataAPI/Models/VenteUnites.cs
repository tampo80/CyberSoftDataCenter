using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class VenteUnites
    {
        public int VenteUnitesID { get; set; }
        public DateTime HeureVente { get; set; }
        public DateTime DateVente { get; set; }
        public string Clients { get; set; }
        public int TypeTarification { get; set; }
        public string Users { get; set; }
        public double MontantAchat { get; set; }
        public int HeuresAchete { get; set; }
        public int CybersCentersID { get; set; }
        public int OperationID { get; set; }
        //Navigation

        virtual  public CybersCenters CybersCenters { get; set; }

    }
}
