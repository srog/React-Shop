using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Cart
{
    public interface IGetCart
    {
        CartDTO Get(int customerId);
    }
}
