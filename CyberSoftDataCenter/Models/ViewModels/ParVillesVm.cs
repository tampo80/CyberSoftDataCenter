using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models.ViewModels
{
    public class ParVillesVm
    {
        public int Id { get; set; }
        public string Ville { get; set; }

        public double Solde { get; set; }

        public double Taux { get; set; }
    }
}
