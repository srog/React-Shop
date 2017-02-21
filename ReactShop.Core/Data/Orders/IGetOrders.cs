using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Orders
{
    public interface IGetOrders
    {
        IEnumerable<OrderDTO> GetAll();
        OrderDTO GetById(int id);
        IEnumerable<OrderDTO> GetByCustomer(int customerId);
    }
}
