using System;
using System.Collections.Generic;

namespace CyberSoftDataCenter.Models
{
    public class VenteProduits
    {
        public VenteProduits()
        {
            this.DetaillesVentes = new HashSet<DetaillesVentes>();
        }
        public int VenteProduitsID { get; set; }
        public DateTime DateOperation { get; set; }

        public string Montant { get; set; }
        public string Remise { get; set; }
        public string Users { get; set; }
        public Guid RefVente { get; set; }
        public int CybersCentersID { get; set; }


        public virtual ICollection<DetaillesVentes> DetaillesVentes { get; set; }
        virtual public CybersCenters CybersCenters { get; set; }
    }
}