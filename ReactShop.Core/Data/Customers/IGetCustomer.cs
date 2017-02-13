using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public interface IGetCustomer
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO Get(int id);
    }
}
