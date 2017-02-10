using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public interface IGetCustomer
    {
        IEnumerable<CustomerDTO> Get();
        CustomerDTO Get(int id);
    }
}
