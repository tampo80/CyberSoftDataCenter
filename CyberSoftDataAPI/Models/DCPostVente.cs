using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataAPI.Models
{

    [Serializable]

    public class DCPostVente
    {
        public int Id { get; set; }
        public List<DCventesInternet> Ventes { get; set; }
    }
}
