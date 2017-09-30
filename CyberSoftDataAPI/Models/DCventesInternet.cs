using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataAPI.Models
{

   

    public class DCventesInternet
    {
        public int id { get; set; }
        public string ville { get; set; }
        public string Cybercenter { get; set; }
        public DateTime DateOperation { get; set; }
        public int TypeTarififcation { get; set; }
        public string Users { get; set; }
        public string MontantAchat { get; set; }
        public int HeuresAchete { get; set; }
        public string Client { get; set; }
        public string Pays { get; set; }
        public string Tel { get; set; }
    }
}
