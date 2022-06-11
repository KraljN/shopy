using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Entities
{
    public class Price : Entity
    {
        public decimal PriceAmount { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
