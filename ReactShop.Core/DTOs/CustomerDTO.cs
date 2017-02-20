using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Title { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public CustomerStatusEnum Status { get; set; }

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
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                Address3 = customer.Address3,
                Postcode = customer.Postcode,
                Title = customer.Title,
                Email = customer.Email,
                Telephone = customer.Telephone,
                Status = customer.Status
            };
        }

        public static explicit operator CustomerDTO(Customer customer)
        {
            return FromCustomer(customer);
        }
    }
}
