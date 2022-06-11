using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries
{
    public class PagedResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);
        public int ItemsPerPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
