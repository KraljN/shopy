using Domen.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Entities
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
