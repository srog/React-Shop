using ReactShop.Core;
using ReactShop.Core.DTOs;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Cart;

namespace ReactShop.Web.WebApi
{
    public class CartController : ApiController
    {
        private readonly IGetCart _getCart;
        private readonly ISaveCartItem _saveCart;

        public CartController()
        {
            _getCart = AutoFacHelper.Resolve<IGetCart>();
            _saveCart = AutoFacHelper.Resolve<ISaveCartItem>();
        }

        // POST: api/Cart
        [OutputCache(NoStore = true)]
        [ValidateHttpAntiForgeryToken]
        public CartDTO Post([FromBody]CartItemDTO value)
        {
            var cart = _getCart.Get(Identity.LoggedInUserId);
            var cartItemDTO = cart.CartItems.FirstOrDefault(i => i.SKU == value.SKU);
            if (cartItemDTO != null)
            {
                cartItemDTO.Quantity = value.Quantity;
                _saveCart.Save(cartItemDTO.ToCartItem());
                if (cartItemDTO.Quantity == 0)
                {
                    _saveCart.Remove(cartItemDTO.ToCartItem());
                }

                var recalculatedCart = _getCart.Get(Identity.LoggedInUserId);
                return recalculatedCart;
            }
            else
            {
                return cart;
            }
        }
    }
}
