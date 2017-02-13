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
        private readonly ISaveCart _saveCart;

        public CartController()
        {
            _checkoutManager = AutoFacHelper.Resolve<ICheckoutManager>();
            _getCart = AutoFacHelper.Resolve<IGetCart>();
            _saveCart = AutoFacHelper.Resolve<ISaveCart>();
        }

        // POST: api/Cart
        [OutputCache(NoStore = true)]
        [ValidateHttpAntiForgeryToken]
        public CartDTO Post([FromBody]CartItemDTO value)
        {
            var cart = _getCart.Get(_checkoutManager.GetCustomer());
            var cartItemDTO = cart.CartItems.SingleOrDefault(i => i.SKU == value.SKU);
            if (cartItemDTO != null)
            {
                cartItemDTO.Quantity = value.Quantity;
                _saveCart.Save(cartItemDTO.ToCartItem());
                if (cartItemDTO.Quantity == 0)
                {
                    _saveCart.Remove(cartItemDTO.ToCartItem());
                }

                var recalculatedCart = _getCart.Get(_checkoutManager.GetCustomer());
                return recalculatedCart;
            }
            else
            {
                return cart;
            }
        }
    }
}
