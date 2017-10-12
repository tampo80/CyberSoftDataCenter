using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Tarifs
    {
        public Tarifs()
        {
            this.DetailTarifs = new HashSet<DetailTarifs>();
        }

        public int TarifsId { get; set; }
        public int TypeCybersId { get; set; }
        public DateTime EpireDate { get; set; }
        public string TarificationName { get; set; }

        public bool Isdefault { get; set; }


        //navigation**

        public virtual TypeCybers TypeCybers { get; set; }
        public virtual ICollection<DetailTarifs> DetailTarifs { get; set; }
    }
}
