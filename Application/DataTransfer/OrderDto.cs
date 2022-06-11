using Domen.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DataTransfer
{
    public class OrderDto : BaseIdDto
    {
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string UserInfo { get; set; }
        public int UserId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public decimal? TotalPrice { get; set; }
    }
}
