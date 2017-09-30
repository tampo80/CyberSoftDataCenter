using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataAPI.Models
{
    public class DCVente
    {
        public DCVente()
        {
            this.DCdetailVente = new List<Models.DCdetailVente>();
        }

        public int VenteProduitsID { get; set; }
        public DateTime DateOperation { get; set; }

        public string Montant { get; set; }
        public string Remise { get; set; }
        public string Users { get; set; }
        public Guid RefVente { get; set; }
        public string Ville { get; set; }
        public string Cybercenter { get; set; }
        public string Pays { get; set; }
        public string Tel { get; set; }
        public List<DCdetailVente> DCdetailVente { get; set; }
    }
}
