﻿using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Cart
{
    public class GetCartItem : IGetCartItem
    {
        public IEnumerable<CartItem> GetAllForCustomer(int customerId)
        {
            using (var db = new Context())
            {
                var cartItems = db.CartItem
                    .Where(ci => ci.CustomerId == customerId)
                    .ToList();

                return cartItems;
            }
        }

        public CartItem GetById(int id)
        {
            using (var db = new Context())
            {
                return db.CartItem.FirstOrDefault(ci => ci.Id == id);
            }
        }
    }
}