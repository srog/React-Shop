﻿using System;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Orders
{
    public class CreateOrder : ICreateOrder
    {
        public int Create(CartDTO cart)
        {
            var result = 0;
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var newOrder = db.Order.Add(new Order
                    {
                        CustomerId = cart.CustomerId,
                        DatePlaced = DateTime.Now,
                        Status = 1, 
                        TotalPrice = cart.Total
                    });

                    db.SaveChanges();

                    foreach (var item in cart.CartItems)
                    {
                        db.OrderItem.Add(new OrderItem
                        {
                            Description = item.Description,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            OrderId = newOrder.Id, 
                            Status = 1
                        });
                    }

                    result = db.SaveChanges();
                    transaction.Commit();
                }
            }
            return result;
        }
    }
}