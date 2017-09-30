using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Pubs
    {
        public int PubsId { get; set; }
        public string Client { get; set; }
        public FormatPub Format { get; set; }
        public DateTime DateCreaton { get; set; }
        public DateTime FinContrat { get; set; }
        public string Url { get; set; }
        public bool Statut { get; set; }
    }
}
