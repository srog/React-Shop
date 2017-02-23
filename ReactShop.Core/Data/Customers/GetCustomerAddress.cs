using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class GetCustomerAddress : IGetCustomerAddress
    {
        public IEnumerable<CustomerAddress> GetAddressesForCustomer(int customerId)
        {
            using (var db = new Context())
            {
                return db.CustomerAddress.Where(c => c.CustomerId == customerId).ToList();
            }
        }

        public CustomerAddressDTO GetCustomerAddressById(int id)
        {
            using (var db = new Context())
            {
                var address = db.CustomerAddress.FirstOrDefault(c => c.Id == id);
                return CustomerAddressDTO.FromCustomerAddress(address);
            }
        }

    }
}