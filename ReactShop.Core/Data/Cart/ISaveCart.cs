using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Cart
{
    public interface ISaveCart
    {
        void Save(CartItem cartItem);
        void Remove(CartItem cartItem);
    }
}
