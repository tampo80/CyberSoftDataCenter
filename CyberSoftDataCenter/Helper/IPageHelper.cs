using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Helper
{
    public interface IPageHelper<T>
    {
        IResultSet<T> GetPage(IQueryable<T> items, int pageNumber);
    }
}

