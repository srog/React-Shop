using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Orders
{
    public class GetOrders : IGetOrders
    {
        public IEnumerable<OrderDTO> GetAll()
        {
            using (var db = new Context())
            {
                return db.Order.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    TotalPrice = o.TotalPrice,
                    DatePlaced = o.DatePlaced,
                    Status = o.Status,
                    DeliveryAddressId = o.DeliveryAddressId,
                    PaymentOptionId = o.PaymentOptionId
                }).ToList();
            }
        }

        public OrderDTO GetById(int id)
        {
            return GetAll().FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<OrderDTO> GetByCustomer(int customerId)
        {
            return GetAll().Where(o => o.CustomerId == customerId).ToList();
        }
    }
}