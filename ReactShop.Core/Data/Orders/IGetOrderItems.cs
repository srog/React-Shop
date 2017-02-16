using System.Collections.Generic;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Orders
{
    public interface IGetOrderItems
    {
        IEnumerable<OrderItem> Get(int orderId);
        OrderItem GetById(int id);
    }
}
