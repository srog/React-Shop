using System.Collections.Generic;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public interface IGetCustomerAddress
    {
        IEnumerable<CustomerAddress> GetAddressesForCustomer(int customerId);
        CustomerAddressDTO GetCustomerAddressById(int id);
    }
}
