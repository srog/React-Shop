using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ReactShop.Core
{
    public class CheckoutManager : ICheckoutManager
    {
        private string serverFilePath;

        public CheckoutManager()
        {
        }

        public CheckoutManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
        }
    

        public CheckoutSummaryDTO GetCheckoutSummary()
        {
            var cartItems = GetCartItems();
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CheckoutSummaryDTO
            {
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
                Total = total,
                CustomerInfo = null, // _getCustomer.Get(1),
                CartItems = cartItems
            };
        }
        
        public void SaveCart(CartItemDTO newOrEditItem)
        {
            try
            {
                if (newOrEditItem.Quantity < 0)
                    newOrEditItem.Quantity = 0;

                using (var db = new Context())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var product = db.Product.Where(p => p.SKU == newOrEditItem.SKU).Single();

                        var cartItem =
                            (from ci in db.CartItem
                             join p in db.Product on ci.ProductId equals p.Id
                             where p.SKU == newOrEditItem.SKU
                             select ci)
                            .SingleOrDefault();

                        if (cartItem != null)
                        {
                            if (newOrEditItem.Quantity == 0)
                                db.CartItem.Remove(cartItem);
                            else
                            {
                                cartItem.Quantity = newOrEditItem.Quantity;
                                cartItem.Product = product;
                            }
                        }
                        else
                        {
                            db.CartItem.Add(new CartItem
                            {
                                Product = product,
                                Quantity = newOrEditItem.Quantity
                            });
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }
    
        private List<CartItemDTO> GetCartItems()
        {
            using (var db = new Context())
            {
                return 
                    (from ci in db.CartItem
                    from p in db.Product.Where(p => p.Id == ci.ProductId)
                    select new { ci, p })
                    .Select(i =>
                    new CartItemDTO
                    {
                        Id =  i.ci.Id,
                        SKU = i.p.SKU,
                        SmallImagePath = i.p.SmallImagePath,
                        Description = i.p.Description,
                        Price = i.p.Price,
                        Quantity = i.ci.Quantity
                    }).ToList();
            }
        }
    }
}
