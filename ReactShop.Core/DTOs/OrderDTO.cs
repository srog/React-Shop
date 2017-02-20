using System;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
