using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class DetaillesVentes
    {
        public int DetaillesVentesID { get; set; }
        public string prixVente { get; set; }
        public string Produits { get; set; }
        public int Quantite { get; set; }
        public int VenteProduitsID { get; set; }
        public string Montant { get; set; }
        public virtual VenteProduits VenteProduits { get; set; }
       
    }
}
