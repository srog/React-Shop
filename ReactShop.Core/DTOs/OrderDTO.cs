using System;
using System.ComponentModel;
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

        public string CustomerName
        {
            get
            {
                var customer = _getCustomers.GetById(CustomerId);
                var displayName = customer.Title + ". " + customer.ForeName + " " + customer.Surname;
                return displayName;
            } 
        }
    }
}
