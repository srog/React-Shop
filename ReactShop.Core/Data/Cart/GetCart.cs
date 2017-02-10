using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Cart
{
    public class GetCart : IGetCart
    {
        public CartDTO Get(IEnumerable<CartItemDTO> cartItems)
        {
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CartDTO
            {
                Subtotal = subtotal,
                DiscountRate = discountRule.Rate * 100M,
                DiscountValue = discountValue,
                Total = total,
                CartItems = cartItems.ToList(),
                CustomerId = 1
            };
        }

        public CartDTO Get()
        {
            return new CartDTO();
        }
    }
}