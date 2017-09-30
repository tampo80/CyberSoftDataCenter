using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Helper
{
    public interface IResultSet<T>
    {
        IEnumerable<T> Items { get; set; }

        Pager Pager { get; }
    }
}
