using System.Collections.Generic;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class CustomerDTO
    {
        private static readonly IGetCustomer _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Title { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public CustomerStatusEnum Status { get; set; }
        public bool IsAdmin { get; set; }

        public IEnumerable<CustomerAddress> Addresses { get; set; }

        public string DisplayName
        {
            get { return Title + " " + ForeName + " " + Surname; }
        }

        public static CustomerDTO FromCustomer(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Username = customer.Username,
                Password = customer.Password,
                ForeName = customer.ForeName,
                Surname = customer.Surname,
                Title = customer.Title,
                Email = customer.Email,
                Telephone = customer.Telephone,
                Status = customer.Status,
                IsAdmin = customer.IsAdmin,
                Addresses = _getCustomer.GetAddressesForCustomer(customer.Id)
            };
        }

        public static explicit operator CustomerDTO(Customer customer)
        {
            return FromCustomer(customer);
        }
    }
}
