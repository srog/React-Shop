using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Orders
{
    public interface ICreateOrder
    {
        int Create(CartDTO cart);
    }
}
