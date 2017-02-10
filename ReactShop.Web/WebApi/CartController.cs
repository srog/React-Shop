using ReactShop.Core;
using ReactShop.Core.DTOs;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ReactShop.Core.Data.Cart;

namespace ReactShop.Web.WebApi
{
    public class CartController : ApiController
    {
        private readonly ICheckoutManager _checkoutManager;
        private readonly IGetCart _getCart;

        public CartController()
        {
            _checkoutManager = AutoFacHelper.Resolve<ICheckoutManager>();
            _getCart = AutoFacHelper.Resolve<IGetCart>();

        }

        // POST: api/Cart
        [OutputCache(NoStore = true)]
        [ValidateHttpAntiForgeryToken]
        public CartDTO Post([FromBody]CartItemDTO value)
        {
            var cart = _getCart.Get();
            var cartItem = cart.CartItems.SingleOrDefault(i => i.SKU == value.SKU);
            if (cartItem != null)
             {
                cartItem.Quantity = value.Quantity;
                var recalculatedCart = _getCart.Get(cart.CartItems);

                _checkoutManager.SaveCart(cartItem);
                return recalculatedCart;
            }
            else
            {
                return cart;
            }
        }
    }
}
