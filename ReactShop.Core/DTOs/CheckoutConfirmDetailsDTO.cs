using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactShop.Core.DTOs
{
    public class CheckoutConfirmDetailsDTO
    {
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }

        public CustomerDTO CustomerInfo { get; set; }
        public int DeliveryAddressId { get; set; }
        public IEnumerable<CartItemDTO> CartItems { get; set; }
    }
}
