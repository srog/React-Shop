using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public class GetCustomer : IGetCustomer
    {
        public IEnumerable<CustomerDTO> Get()
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
                            Postcode = i.Postcode
                        }).ToList();
            }
        }

        public CustomerDTO Get(int id)
        {
            var customer = Get().FirstOrDefault(c => c.Id == id);
            return customer;
        }
    }
}