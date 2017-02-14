using System;
using System.Linq;
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
                    db.Order.Add(new Order
                    {
                        CustomerId = cart.CustomerId,
                        DatePlaced = DateTime.Now,
                        Products = cart.CartItems.Select(ci => ci.Id),
                        TotalPrice = cart.Total
                    });

                    result = db.SaveChanges();
                    transaction.Commit();
                }
            }
            return result;
        }
    }
}