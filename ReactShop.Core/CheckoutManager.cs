using ReactShop.Core.DTOs;
using ReactShop.Core.Data.Cart;
using ReactShop.Core.Data.Customers;

namespace ReactShop.Core
{
    public class CheckoutManager : ICheckoutManager
    {
        private readonly IGetCart _getCart;
        private readonly IGetCustomer _getCustomer;
        private string serverFilePath;

     
        public CheckoutManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
            _getCart = AutoFacHelper.Resolve<IGetCart>();
            _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        }

        public int GetCustomer()
        {
            return 1;
        }

        public CheckoutSummaryDTO GetCheckoutSummary()
        {
            var cart = _getCart.Get(GetCustomer());
            // create order

            return new CheckoutSummaryDTO
            {
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
                Total = cart.Total,
                CustomerInfo = _getCustomer.Get(GetCustomer()),
                CartItems = cart.CartItems
            };
        }
        
        //public void SaveCart(CartItemDTO newOrEditItem)
        //{
        //    try
        //    {
        //        if (newOrEditItem.Quantity < 0)
        //            newOrEditItem.Quantity = 0;

        //        using (var db = new Context())
        //        {
        //            using (var transaction = db.Database.BeginTransaction())
        //            {
        //                var product = db.Product.Where(p => p.SKU == newOrEditItem.SKU).Single();

        //                var cartItem =
        //                    (from ci in db.CartItem
        //                     join p in db.Product on ci.ProductId equals p.Id
        //                     where p.SKU == newOrEditItem.SKU
        //                     select ci)
        //                    .SingleOrDefault();

        //                if (cartItem != null)
        //                {
        //                    if (newOrEditItem.Quantity == 0)
        //                        db.CartItem.Remove(cartItem);
        //                    else
        //                    {
        //                        cartItem.Quantity = newOrEditItem.Quantity;
        //                        cartItem.Product = product;
        //                    }
        //                }
        //                else
        //                {
        //                    db.CartItem.Add(new CartItem
        //                    {
        //                        Product = product,
        //                        Quantity = newOrEditItem.Quantity
        //                    });
        //                }

        //                db.SaveChanges();
        //                transaction.Commit();
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}",
        //                                        validationError.PropertyName,
        //                                        validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //}
    
      
    }
}
