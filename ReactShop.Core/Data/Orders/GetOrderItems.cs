using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Orders
{
    public class GetOrderItems : IGetOrderItems
    {
        public IEnumerable<OrderItem> Get(int orderId)
        {
            using (var db = new Context())
            {
                return db.OrderItem.Where(oi => oi.OrderId == orderId).ToList();
            }
        }

        public OrderItem GetById(int id)
        {
            using (var db = new Context())
            {
                return db.OrderItem.FirstOrDefault(oi => oi.Id == id);
            }
        }
    }
}