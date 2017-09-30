using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class BkpDataBases
    {
        public int BkpDataBasesID { get; set; }
        public int CyberCenterID { get; set; }
        public DateTime DateBKP { get; set; }
        //navigation
        virtual public CybersCenters CybersCenters { get; set; }
    }
}
