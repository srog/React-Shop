using System.Collections.Generic;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Orders
{
    public interface IGetOrders
    {
        IEnumerable<Order> Get();
    }
}
