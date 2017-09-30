using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Pays
    {
        public Pays()
        {
            this.Villes = new HashSet<Villes>();
            
        }

        public int PaysID { get; set; }
        public string Nom { get; set; }

        //Navigaion
        public virtual ICollection<Villes> Villes { get; set; }
        
    }
}
