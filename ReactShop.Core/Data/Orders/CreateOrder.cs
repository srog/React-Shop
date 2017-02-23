using System;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

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
                        Status = OrderStatusEnum.Booked, 
                        TotalPrice = cart.Total,
                        DeliveryAddressId = cart.DeliveryAddressId,
                        PaymentOptionId = cart.PaymentOptionId
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
                            Quantity = item.Quantity,
                            Status = OrderStatusEnum.Booked
                        });
                    }
                    db.SaveChanges();

                    // remove the cart items!
                    foreach (var cartItemDto in cart.CartItems)
                    {
                        var cartItem = cartItemDto.ToCartItem();
                        db.CartItem.Attach(cartItem);
                        db.CartItem.Remove(cartItem);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    result = newOrder.Id;
                }
            }
            return result;
        }
    }
}