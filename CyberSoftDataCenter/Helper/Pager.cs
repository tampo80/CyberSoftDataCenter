using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Helper
{
    public class Pager
    {
        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }

        public int TotalRecords { get; set; }
    }
}
