using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class ProductSearchDto : PagedSearch
    {
        public IEnumerable<int> CategoryIds { get; set; } = new List<int>();
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
