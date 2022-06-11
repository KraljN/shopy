using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class LogSearchDto : PagedSearch
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
