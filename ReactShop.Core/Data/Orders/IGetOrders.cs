using System.Collections.Generic;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Orders
{
    public interface IGetOrders
    {
        IEnumerable<OrderDTO> Get();
        OrderDTO GetById(int id);
        IEnumerable<OrderDTO> GetByCustomer(int customerId);
    }
}
