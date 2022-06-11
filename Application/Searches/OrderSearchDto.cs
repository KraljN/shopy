using Domen.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class OrderSearchDto : PagedSearch
    {
        public DateTime? OrderDateMin { get; set; }
        public DateTime? OrderDateMax { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
        public IEnumerable<int> ProductIds { get; set; } = new List<int>();
        public int? OrderItemQuantityMin { get; set; }
        public int? OrderItemQuantityMax { get; set; }
        public int? OrderItemsMin { get; set; }
        public int? OrderItemsMax { get; set; }
        public decimal? TotalPriceMin { get; set; }
        public decimal? TotalPriceMax { get; set; }
    }
}
