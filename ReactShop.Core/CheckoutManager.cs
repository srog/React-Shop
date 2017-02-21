using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Data.Orders;

namespace ReactShop.Core
{
    public class CheckoutManager : ICheckoutManager
    {
        private readonly IGetOrders _getOrder;
        private readonly IGetOrderItems _getOrderItems;
        private readonly IGetCustomer _getCustomer;
        private string serverFilePath;
     
        public CheckoutManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
            _getOrder = AutoFacHelper.Resolve<IGetOrders>();
            _getOrderItems = AutoFacHelper.Resolve<IGetOrderItems>();
            _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        }
        
        public CheckoutSummaryDTO GetCheckoutSummary(int newOrderId)
        {
            var order = _getOrder.GetById(newOrderId);
            var customer = _getCustomer.GetById(order.CustomerId);
            var orderItems = _getOrderItems.Get(newOrderId).Select(oi => new OrderItemDTO()
            {
                Quantity = oi.Quantity,
                Description = oi.Description,
                Price = oi.Price,
                ProductId = oi.ProductId,
                Status = oi.Status,
                OrderId = oi.OrderId
            });
   
            return new CheckoutSummaryDTO
            {
                OrderNumber = newOrderId.ToString("000000000"),
                DeliveryUpToNWorkingDays = 4,
                Total = order.TotalPrice,
                CustomerInfo = CustomerDTO.FromCustomer(customer),
                DeliveryAddress = _getCustomer.GetCustomerAddressById(order.DeliveryAddressId),
                OrderItems = orderItems
            };
        }
      
    }
}
