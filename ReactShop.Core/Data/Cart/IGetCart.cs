using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Cart
{
    public interface IGetCart
    {
        CartDTO Get(IEnumerable<CartItemDTO> cartItems);
        CartDTO Get();
    }
}
