using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class GetCustomer : IGetCustomer
    {
        public IEnumerable<CustomerDTO> GetAll()
        {
            using (var db = new Context())
            {
                return db.Customer
                    .Select(i =>
                        new CustomerDTO
                        {
                            Id = i.Id,
                            Title = i.Title,
                            ForeName = i.ForeName,
                            Surname = i.Surname,
                            Address1 = i.Address1,
                            Address2 = i.Address2,
                            Address3 = i.Address3,
                            Postcode = i.Postcode,
                            Email = i.Email,
                            Telephone = i.Telephone
                        }).ToList();
            }
        }

        public CustomerDTO GetById(int id)
        {
            using (var db = new Context())
            {
                Customer customer = db.Customer.FirstOrDefault(c => c.Id == id);
                return CustomerDTO.FromCustomer(customer);
            }
        }
    }
}