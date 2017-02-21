using System.Collections.Generic;

namespace ReactShop.Core.DTOs
{
    public class CartDTO
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryAddressId { get; set; }

        public List<CartItemDTO> CartItems { get; set; }
    }
}