﻿using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Data.Products;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Cart
{
    public class GetCart : IGetCart
    {
        private readonly IGetCartItem _getCartItem;
        private readonly IGetProducts _getProduct;
        private readonly IGetCustomerAddress _getCustomerAddress;
        private readonly IGetPaymentOption _getPaymentOption;

        public GetCart()
        {
            _getCartItem = AutoFacHelper.Resolve<IGetCartItem>();
            _getProduct = AutoFacHelper.Resolve<IGetProducts>();
            _getCustomerAddress = AutoFacHelper.Resolve<IGetCustomerAddress>();
            _getPaymentOption = AutoFacHelper.Resolve<IGetPaymentOption>();
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
            var addresses = _getCustomerAddress.GetAddressesForCustomer(customerId);
            var paymentOptions = _getPaymentOption.GetPaymentOptionsForCustomer(customerId);

            return new CartDTO
            {
                Subtotal = subtotal,
                DiscountRate = discountRule.Rate * 100M,
                DiscountValue = discountValue,
                Total = total,
                CustomerId = customerId,
                CartItems = cartItemDTOList,
                DeliveryAddressId = addresses?.FirstOrDefault().Id ?? 0,
                PaymentOptionId = paymentOptions?.FirstOrDefault().Id ?? 0
            };
        }
    }
}