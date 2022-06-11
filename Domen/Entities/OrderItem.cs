using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Entities
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
