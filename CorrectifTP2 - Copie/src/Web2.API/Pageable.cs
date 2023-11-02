using System.Collections.Generic;
using System.Diagnostics;

namespace Todolist.API
{
    public class Pageable<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public List<T> data { get; set; }
    }
}