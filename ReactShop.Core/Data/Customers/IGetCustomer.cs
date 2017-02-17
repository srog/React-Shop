using System.Collections.Generic;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public interface IGetCustomer
    {
        IEnumerable<CustomerDTO> GetAll();
        Customer GetById(int id);
        Customer GetByUsernameAndPassword(string username, string password);
    }
}
