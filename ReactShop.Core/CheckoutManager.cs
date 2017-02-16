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
        
        public CheckoutSummaryDTO GetCheckoutSummary()
        {
            var cart = _getCart.Get(Common.Identity.LoggedInUserId);
            var customer = _getCustomer.GetById(Common.Identity.LoggedInUserId);

            return new CheckoutSummaryDTO
            {
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
                Total = cart.Total,
                CustomerInfo = CustomerDTO.FromCustomer(customer),
                CartItems = cart.CartItems
            };
        }
      
    }
}
