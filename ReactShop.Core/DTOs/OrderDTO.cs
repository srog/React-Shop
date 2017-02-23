using System;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class OrderDTO
    {
        private readonly IGetCustomer _getCustomers;
        public OrderDTO()
        {
            _getCustomers = AutoFacHelper.Resolve<IGetCustomer>();
        }

        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int DeliveryAddressId { get; set; }
        public int PaymentOptionId { get; set; }
        public string CustomerName
        {
            get
            {
                var customerDto = CustomerDTO.FromCustomer(_getCustomers.GetById(CustomerId));
                return customerDto.DisplayName;
            } 
        }
    }
}
