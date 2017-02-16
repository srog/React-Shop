using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.Data.Products;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Cart
{
    public class GetCart : IGetCart
    {
        private readonly IGetCartItem _getCartItem;
        private readonly IGetProducts _getProduct;

        public GetCart()
        {
            _getCartItem = AutoFacHelper.Resolve<IGetCartItem>();
            _getProduct = AutoFacHelper.Resolve<IGetProducts>();
        }
        public CartDTO Get(int customerId)
        {
            var cartItems = _getCartItem.GetAllForCustomer(customerId);

            var cartItemDTOList = new List<CartItemDTO>();
            foreach (var cartItem in cartItems)
            {
                var product = _getProduct.GetById(cartItem.ProductId);

                cartItemDTOList.Add(new CartItemDTO
                {
                    Id = cartItem.Id,
                    Description = product.Description,
                    LargeImagePath = product.LargeImagePath,
                    SmallImagePath = product.SmallImagePath,
                    Price = product.Price,
                    Quantity = cartItem.Quantity,
                    SKU = product.SKU,
                    CustomerId = customerId,
                    ProductId = product.Id
                });
            }

            var subtotal = cartItemDTOList.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CartDTO
            {
                Subtotal = subtotal,
                DiscountRate = discountRule.Rate * 100M,
                DiscountValue = discountValue,
                Total = total,
                CustomerId = customerId,
                CartItems = cartItemDTOList
            };
        }
    }
}