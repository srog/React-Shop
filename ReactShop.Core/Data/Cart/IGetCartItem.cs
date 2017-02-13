using System.Collections.Generic;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Cart
{
    public interface IGetCartItem
    {
        IEnumerable<CartItem> GetAllForCustomer(int customerId);
        CartItem GetById(int id);
    }
}
