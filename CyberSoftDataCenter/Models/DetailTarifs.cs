using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
     public class DetailTarifs
        {
            public int DetailTarifsId { get; set; }
            public int TrancheMin { get; set; }
            public string Cout { get; set; }
            public int validiteEnJour { get; set; }
            public int TarifsId { get; set; }
            //Navigation

            public Tarifs Tarifs { get; set; }
        }
    
}
