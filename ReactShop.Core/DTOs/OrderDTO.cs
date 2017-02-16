using System;
using System.Collections.Generic;

namespace ReactShop.Core.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public int Status { get; set; }
        public IEnumerable<int> OrderItemIds { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
